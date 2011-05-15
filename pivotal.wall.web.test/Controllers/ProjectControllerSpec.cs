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
using pivotal.wall.web.test.Fixtures;
using Ploeh.AutoFixture;
using System.Linq;
using Should;

namespace pivotal.wall.web.test.Controllers
{
    [TestFixture]
    public class ProjectController_when_there_are_multiple_matches : ProjectControllerSpecBase
    {
        public override void Given()
        {
            ServiceReturnsProjectWithStories(new Dictionary<string, Story>
            {
                {"pip", NewStory.With.Points(2).State(State.Rejected).Title("pip the troll").Labels("this is a label")},
                {"vic", NewStory.With.Points(2).State(State.Accepted).Title("victor von doom")},
                {"adam", NewStory.With.Points(2).State(State.Finished).Title("adam warlock").Labels("no match", "this is a label")},
                {"gamora", NewStory.With.Points(2).State(State.Finished).Title("gamora")},
                {"thanos", NewStory.With.Points(5).State(State.Finished).Title("thanos of titan").Labels("this is a label")}
            });

            BuilderReturnsColumns(new List<Column>
            {
                new Column{ State = State.Finished.ToString() }, 
                new Column{ Label = "this is a label" }, 
                new Column{ State = State.Rejected.ToString() }
            });

            _controller = new ProjectController(_pivotalService, _columnBuilder);
        }

        public override void When()
        {
            _result = _controller.View(123);
        }
        
        [Test]
        public void matches_nearer_to_the_end_win()
        {
            StoriesAtColumn(0).Count().ShouldEqual(1);
            StoriesAtColumn(0).First().ShouldMatch(_stories["gamora"]);

            StoriesAtColumn(1).Count().ShouldEqual(2);
            StoriesAtColumn(1).ElementAt(0).ShouldMatch(_stories["adam"]);
            StoriesAtColumn(1).ElementAt(1).ShouldMatch(_stories["thanos"]);

            StoriesAtColumn(2).Count().ShouldEqual(1);
            StoriesAtColumn(2).First().ShouldMatch(_stories["pip"]);
        }
    }

    [TestFixture]
    public class ProjectControllerSpec_when_getting_view : ProjectControllerSpecBase
    {
        public override void Given()
        {
            ServiceReturnsProjectWithStories(new Dictionary<string, Story>
            {
                {"pip", NewStory.With.Points(2).State(State.Rejected).Title("pip the troll")},
                {"vic", NewStory.With.Points(2).State(State.Accepted).Title("victor von doom")},
                {"adam", NewStory.With.Points(2).State(State.Finished).Title("adam warlock")},
                {"gamora", NewStory.With.Points(2).State(State.Finished).Title("gamora")},
                {"thanos", NewStory.With.Points(5).State(State.Started).Title("thanos of titan").Labels("this is a label")}
            });

            BuilderReturnsColumns(new List<Column>
            {
                new Column{ Label = "this is a label" }, 
                new Column{ State = State.Rejected.ToString() }, 
                new Column{ State = State.Finished.ToString() }
            });

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
    }

    public abstract class ProjectControllerSpecBase : Spec
    {
        protected ActionResult _result;
        protected IDictionary<string, Story> _stories;
        protected PivotalService _pivotalService;
        protected PivotalColumnBuilder _columnBuilder;
        protected ProjectController _controller;

        protected void BuilderReturnsColumns(List<Column> columns)
        {
            _columnBuilder = A.Fake<PivotalColumnBuilder>();

            A.CallTo(() => _columnBuilder.GetColumns()).Returns(columns);
        }

        protected void ServiceReturnsProjectWithStories(Dictionary<string, Story> stories)
        {
            _stories = stories;

            var project = Fixture.Build<Project>()
                .With(x => x.Name, "project name")
                .With(x => x.Stories, _stories.Select(s => s.Value))
                .CreateAnonymous();

            _pivotalService = A.Fake<PivotalService>();

            A.CallTo(_pivotalService).WithReturnType<Project>().Returns(project);
        }

        public abstract override void Given();
        public abstract override void When();

        protected IEnumerable<StoryViewModel> StoriesAtColumn(int index)
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