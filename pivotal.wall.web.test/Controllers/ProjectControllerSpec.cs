using System.Collections.Generic;
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
using Should;

namespace pivotal.wall.web.test.Controllers
{
    [TestFixture]
    public class ProjectControllerSpec_when_getting_view : Spec
    {
        private ProjectController _controller;
        private ActionResult _result;
        private Project _project;

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
            GetAssertViewModel();
        }

        [Test]
        public void it_contains_the_data_from_the_project_model()
        {
            var model = GetAssertViewModel();

            model.Name.ShouldBe(_project.Name);
        }

        [Test]
        public void it_contains_stories_from_the_project()
        {
            var model = GetAssertViewModel();

            model.Stories.Count().ShouldBe(3);
        }

        [Test]
        public void stories_contain_title()
        {
            var model = GetAssertViewModel();

            ShouldContainAll(_project.Stories.Select(s => s.Title), model.Stories.Select(s => s.Title));
        }

        [Test]
        public void stories_contain_points()
        {
            var model = GetAssertViewModel();

            ShouldContainAll(_project.Stories.Select(s => s.Points.ToString()), model.Stories.Select(s => s.Points));
        }

        private static void ShouldContainAll<T>(IEnumerable<T> source, IEnumerable<T> dest)
        {
            source.All(s =>
            {
                dest.ShouldContain(s);
                return true;
            });
        }

        private ProjectViewModel GetAssertViewModel()
        {
            return _result.AssertViewRendered().WithViewData<ProjectViewModel>();
        }
    }
}