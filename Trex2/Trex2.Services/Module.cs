using Autofac;
using Trex2.Services.Contracts;
using Trex2.Services.Controllers;
using Trex2.Services.Data;

namespace Trex2.Services
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DetailsContext>().AsSelf().SingleInstance();
            builder.RegisterType<DetailsController>().As<IDetailsController>().SingleInstance();
        }
    }
}