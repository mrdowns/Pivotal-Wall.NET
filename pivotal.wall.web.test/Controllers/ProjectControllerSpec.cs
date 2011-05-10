using System.Collections.Generic;
using System.Web.Mvc;
using FakeItEasy;
using NUnit.Framework;
using MvcContrib.TestHelper;
using pivotal.wall.model;
using pivotal.wall.test.util;
using pivotal.wall.web.Controllers;
using pivotal.wall.web.Helpers;
using pivotal.wall.web.Models;
using Ploeh.AutoFixture;
using System.Linq;
using Should;

namespace pivotal.wall.web.test.Controllers
{
    [TestFixture]
    public class ProjectControllerSpec_when_getting_view_refactor : Spec
    {
        private PivotalService _pivotalService;
        private PivotalColumnBuilder _columnBuilder;
        private ProjectController _controller;
        private ActionResult _result;
        private List<Column> _columns;

        public override void Given()
        {
            _pivotalService = A.Fake<PivotalService>();

            var project = Fixture.Build<Project>()
                .With(x => x.Name, "project name")
                .CreateAnonymous();

            A.CallTo(_pivotalService).WithReturnType<Project>().Returns(project);

            _columnBuilder = A.Fake<PivotalColumnBuilder>();

            _columns = new List<Column>{ new Column(), new Column(), new Column() };

            A.CallTo(() => _columnBuilder.GetColumns()).Returns(_columns);

            _controller = new ProjectController(_pivotalService, _columnBuilder);
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
            _result.AssertViewRendered().WithViewData<ProjectViewModel>().Name.ShouldEqual("project name");
        }

        [Test]
        public void it_requests_the_project_by_id()
        {
            A.CallTo(() => _pivotalService.GetProject(123)).MustHaveHappened();
        }

        [Test]
        public void it_has_three_columns()
        {
            _result.AssertViewRendered().WithViewData<ProjectViewModel>().Columns.Count().ShouldEqual(3);
        }

        [Test]
        public void the_columns_match_the_builder()
        {
            Assert.Fail("need to test");
        }

        [Test]
        public void the_columns_are_in_order()
        {
            Assert.Fail("need to test");
        }

        [Test]
        public void the_columns_contain_matching_stories()
        {
            Assert.Fail("need to test");
        }

        [Test]
        public void the_stories_match_the_repository()
        {
            Assert.Fail("need to test");
        }

        private static void ShouldContainAll<T>(IEnumerable<T> source, IEnumerable<T> dest)
        {
            source.All(s =>
            {
                dest.ShouldContain(s);
                return true;
            });
        }
    }

}