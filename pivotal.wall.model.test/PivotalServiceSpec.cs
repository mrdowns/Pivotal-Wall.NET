using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using pivotal.wall.test.util;
using Ploeh.AutoFixture;
using Should;

namespace pivotal.wall.model.test
{
    [TestFixture]
    public class PivotalServiceSpec_when_getting_a_project : Spec
    {
        private PivotalService _service;
        private Project _result;
        private Project _project;

        public override void Given()
        {
            var repository = A.Fake<IProjectRepository>();

            _project = Fixture.Build<Project>().With(p => p.Stories, Enumerable.Empty<Story>()).CreateAnonymous();

            A.CallTo(() => repository.GetProject(123)).Returns(_project);
            A.CallTo(() => repository.GetStoriesForProject(123)).Returns(Fixture.CreateMany<Story>(3));

            _service = new PivotalService(repository);
        }

        public override void When()
        {
            _result = _service.GetProject(123);
        }

        [Test]
        public void returns_project_from_web()
        {
            _result.Name.ShouldEqual(_project.Name);
        }

        [Test]
        public void returns_stories_in_project()
        {
            _result.Stories.Count().ShouldEqual(3);
        }
    }
}