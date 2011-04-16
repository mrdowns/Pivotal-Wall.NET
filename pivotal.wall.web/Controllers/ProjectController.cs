using System.Web.Mvc;
using pivotal.wall.model;
using pivotal.wall.web.Models;

namespace pivotal.wall.web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly PivotalService _service;

        public ProjectController(PivotalService service)
        {
            _service = service;
        }

        public ActionResult Edit(int id)
        {
            var project = _service.GetProject(id);

            return View(new ProjectViewModel { Name = project.Name });
        }
    }
}