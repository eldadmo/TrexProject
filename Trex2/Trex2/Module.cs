using Autofac;
using Trex2.Contracts;
using Trex2.ViewModels;

namespace Trex2
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShellWindowViewModel>().As<IShellWindowViewModel>().SingleInstance();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>().SingleInstance();
            builder.RegisterType<DetailsViewModel>().As<IDetailsViewModel>().SingleInstance();
        }
    }
}