using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using pivotal.wall.model;
using pivotal.wall.repositories;
using PivotalTracker.FluentAPI.Domain;
using PivotalTracker.FluentAPI.Repository;

namespace pivotal.wall.web
{
    public class AutofacHelper
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<PivotalService>();

            builder.Register(c => new PivotalProjectRepository(new Token("3bbf29f25ccdab6a58a544df12d1d830")));

            return builder.Build();
        }
    }
}