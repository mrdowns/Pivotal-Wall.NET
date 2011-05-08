using System.Web.Mvc;
using FakeItEasy;
using NUnit.Framework;
using MvcContrib.TestHelper;
using pivotal.wall.model;
using pivotal.wall.test.util;
using pivotal.wall.web.Controllers;
using pivotal.wall.web.Models;
using Ploeh.AutoFixture;
using System.Linq;

namespace pivotal.wall.web.test.Controllers
{
    [TestFixture]
    public class ProjectControllerSpec_when_getting_view : Spec
    {
        private ProjectController _controller;
        private ActionResult _result;
        private Project _project;
        private ProjectViewModel _model;

        public override void Given()
        {
            var pivotalService = A.Fake<PivotalService>();
            
            _project = Fixture.Build<Project>()
                .With(p => p.Stories, Fixture.CreateMany<Story>(3))
                .CreateAnonymous();

            A.CallTo(() => pivotalService.GetProject(123)).Returns(_project);

            _controller = new ProjectController(pivotalService);
        }

        public override void When()
        {
            _result = _controller.View(123);
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
            _model = _result.AssertViewRendered().WithViewData<ProjectViewModel>();

            _model.Name.ShouldBe(_project.Name);
        }

        [Test]
        public void it_contains_unstarted_stories_from_the_project()
        {
            _model = _result.AssertViewRendered().WithViewData<ProjectViewModel>();

            _model.Stories.Count().ShouldBe(3);
        }
    }
}