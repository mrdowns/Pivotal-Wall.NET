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
    public class ProjectControllerSpec_when_getting_view : Spec
    {
        private PivotalService _pivotalService;
        private PivotalColumnBuilder _columnBuilder;
        private ProjectController _controller;
        private ActionResult _result;
        private IDictionary<string, Story> _stories;

        public override void Given()
        {
            _stories = new Dictionary<string, Story>
            {
                {"pip", new Story{Points = 4, State = State.Rejected, Title = "pip the troll"}},
                {"vic", new Story{Points = 2, State = State.Accepted, Title = "victor von doom"}},
                {"adam", new Story{Points = 2, State = State.Finished, Title = "adam warlock"}},
                {"gamora", new Story{Points = 2, State = State.Finished, Title = "gamora"}},
                {"thanos", new Story{Points = 5, State = State.Started, Title = "thanos of titan", Labels = new List<string>{"this is a label"}}}
            };

            var project = Fixture.Build<Project>()
                .With(x => x.Name, "project name")
                .With(x => x.Stories, _stories.Select(s => s.Value))
                .CreateAnonymous();

            _pivotalService = A.Fake<PivotalService>();

            A.CallTo(_pivotalService).WithReturnType<Project>().Returns(project);

            var columns = new List<Column>
            {
                new Column{ Label = "this is a label" }, 
                new Column{ State = State.Rejected.ToString() }, 
                new Column{ State = State.Finished.ToString() }
            };

            _columnBuilder = A.Fake<PivotalColumnBuilder>();

            A.CallTo(() => _columnBuilder.GetColumns()).Returns(columns);

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
        public void the_columns_set_titles_and_match_the_order()
        {
            var columns = _result.AssertViewRendered().WithViewData<ProjectViewModel>().Columns;

            columns.ElementAt(0).Title.ShouldEqual("this is a label");
            columns.ElementAt(1).Title.ShouldEqual("Rejected");
            columns.ElementAt(2).Title.ShouldEqual("Finished");
        }

        [Test]
        public void the_columns_contain_matching_stories()
        {
            StoriesAtColumn(0).Count().ShouldEqual(1);
            StoriesAtColumn(0).First().ShouldMatch(_stories["thanos"]);

            StoriesAtColumn(1).Count().ShouldEqual(1);
            StoriesAtColumn(1).First().ShouldMatch(_stories["pip"]);

            StoriesAtColumn(2).Count().ShouldEqual(2);
            StoriesAtColumn(2).ElementAt(0).ShouldMatch(_stories["adam"]);
            StoriesAtColumn(2).ElementAt(1).ShouldMatch(_stories["gamora"]);
        }

        private IEnumerable<StoryViewModel> StoriesAtColumn(int index)
        {
            var columns = _result.AssertViewRendered().WithViewData<ProjectViewModel>().Columns;
            return columns.ElementAt(index).Stories;
        }
    }

    public static class StoryViewModelExtensions
    {
        public static void ShouldMatch(this StoryViewModel vm, Story s)
        {
            vm.Title.ShouldEqual(s.Title);
            vm.State.ShouldEqual(s.State.ToString());
            vm.Points.ShouldEqual(s.Points.ToString());
        }
    }

}