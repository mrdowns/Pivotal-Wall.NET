using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using pivotal.wall.model;
using pivotal.wall.web.Helpers;
using pivotal.wall.web.Models;
using System.Linq;

namespace pivotal.wall.web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly PivotalService _service;
        private readonly PivotalColumnBuilder _columnBuilder;

        public ProjectController(PivotalService service, PivotalColumnBuilder columnBuilder)
        {
            Mapper.CreateMap<Project, ProjectViewModel>();
            Mapper.CreateMap<Story, StoryViewModel>().ForMember(s => s.Owner, s => s.MapFrom(st => st.Owner ?? "Available"));

            _service = service;
            _columnBuilder = columnBuilder;
        }

        public ActionResult View(int id)
        {
            var project = _service.GetProject(id);

            var columns = _columnBuilder.GetColumns();

            var projectViewModel = new ProjectViewModel(project, columns);
            
            return View(projectViewModel);
        }
    }
}