using System.Web.Mvc;
using AutoMapper;
using pivotal.wall.model;
using pivotal.wall.web.Helpers;
using pivotal.wall.web.Models;

namespace pivotal.wall.web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly PivotalService _service;
        private readonly PivotalColumnBuilder _columnBuilder;

        public ProjectController(PivotalService service, PivotalColumnBuilder columnBuilder)
        {
            Mapper.CreateMap<Project, ProjectViewModel>();
            Mapper.CreateMap<Story, StoryViewModel>();

            _service = service;
            _columnBuilder = columnBuilder;
        }

        public ActionResult View(int id)
        {
            var project = _service.GetProject(id);

            var projectViewModel = Mapper.Map<Project, ProjectViewModel>(project);

            var columns = _columnBuilder.GetColumns();

            foreach (var column in columns)
            {
                projectViewModel.Columns.Add(new ColumnViewModel());
            }

            return View(projectViewModel);
        }
    }
}