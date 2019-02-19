using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using Trex2.Contracts;
using Module = Trex2.Module;

namespace RestClient
{
    public class Bootstrapper : BootstrapperBase
    {
        private const string ModuleFilePrefix = "Trex2";

        private IContainer _container;

        protected override void Configure()
        {
            base.Configure();           
            var builder = new ContainerBuilder();

            RegisterTypes(builder);
            RegisterModule(builder);
            _container = builder.Build();
        }

        private void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterModule<Module>();
            builder.RegisterModule<Trex2.Services.Module>();
        }


        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShellWindowViewModel>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return GetAllDllEntries().Select(Assembly.LoadFrom);
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override object GetInstance(Type service, string key)
        {
            return key == null ? _container.Resolve(service) : _container.ResolveKeyed(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var enumerableOfServiceType = typeof(IEnumerable<>).MakeGenericType(service);
            return (IEnumerable<object>)_container.Resolve(enumerableOfServiceType);
        }

        private static string[] GetAllDllEntries()
        {
            var runtimeDir = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(runtimeDir).Where(file => Regex.IsMatch(file, @"^.+\.(exe|dll)$")).Where(x =>
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(x);
                return fileNameWithoutExtension.StartsWith(ModuleFilePrefix, StringComparison.Ordinal);
            }).ToArray();

            return files;
        }
    }
}