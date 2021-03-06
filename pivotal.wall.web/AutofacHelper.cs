using System.Configuration;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using pivotal.wall.model;
using pivotal.wall.repositories;
using pivotal.wall.web.Helpers;
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

            var columns = ConfigurationManager.AppSettings["PivotalColumnList"];

            builder.Register(b => new PivotalColumnBuilder(columns));

            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<PivotalService>();

            var apiKey = ConfigurationManager.AppSettings["PivotalApiKey"];

            var token = new Token(apiKey);

            builder.Register(c => new PivotalProjectRepository(token));
            builder.Register(c => new PivotalStoryRepository(token));

            return builder.Build();
        }
    }
}