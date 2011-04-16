using System.Web.Mvc;
using FakeItEasy;
using NUnit.Framework;
using MvcContrib.TestHelper;
using pivotal.wall.model;
using pivotal.wall.web.Controllers;
using pivotal.wall.web.Models;
using Ploeh.AutoFixture;

namespace pivotal.wall.web.test.Controllers
{
    [TestFixture]
    public class ProjectControllerSpec_when_getting_edit : Spec
    {
        private ProjectController _controller;
        private ActionResult _result;
        private PivotalProject _project;

        public override void Given()
        {
            var pivotalService = A.Fake<PivotalService>();
            
            _project = Fixture.CreateAnonymous<PivotalProject>();

            A.CallTo(() => pivotalService.GetProject(123)).Returns(_project);

            _controller = new ProjectController(pivotalService);
        }

        public override void When()
        {
            _result = _controller.Edit(123);
        }

        [Test]
        public void it_returns_the_default_view()
        {
            _result.AssertViewRendered().ForView(string.Empty);
        }

        [Test]
        public void it_contains_the_project_model()
        {
            _result.AssertViewRendered().WithViewData<ProjectViewModel>();
        }

        [Test]
        public void it_contains_the_data_from_the_project_model()
        {
            var model = _result.AssertViewRendered().WithViewData<ProjectViewModel>();

            model.Name.ShouldBe(_project.Name);
        }
    }
}