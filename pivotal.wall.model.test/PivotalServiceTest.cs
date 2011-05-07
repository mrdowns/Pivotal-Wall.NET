using System;
using FakeItEasy;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Should;

namespace pivotal.wall.model.test
{
    [TestFixture]
    public class PivotalServiceTest_when_getting_a_project : Spec
    {
        private PivotalService _service;
        private PivotalProject _result;
        private PivotalProject _project;

        public override void Given()
        {
            var repository = A.Fake<IProjectRepository>();

            _project = Fixture.CreateAnonymous<PivotalProject>();

            A.CallTo(() => repository.GetProject(123)).Returns(_project);

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
    }
}