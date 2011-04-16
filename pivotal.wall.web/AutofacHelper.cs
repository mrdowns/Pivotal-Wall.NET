using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using pivotal.wall.model;

namespace pivotal.wall.web
{
    public class AutofacHelper
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<PivotalService>();
            return builder.Build();
        }
    }
}