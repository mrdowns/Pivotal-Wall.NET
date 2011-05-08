using System.Web.Mvc;
using AutoMapper;
using pivotal.wall.model;
using pivotal.wall.web.Models;

namespace pivotal.wall.web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly PivotalService _service;

        public ProjectController(PivotalService service)
        {
            Mapper.CreateMap<Project, ProjectViewModel>();
            Mapper.CreateMap<Story, StoryViewModel>();

            _service = service;
        }

        public ActionResult View(int id)
        {
            var project = _service.GetProject(id);

            return View(Mapper.Map<Project, ProjectViewModel>(project));
        }
    }
}