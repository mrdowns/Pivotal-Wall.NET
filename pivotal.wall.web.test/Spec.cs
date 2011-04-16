using NUnit.Framework;
using Ploeh.AutoFixture;

namespace pivotal.wall.web.test
{
    public abstract class Spec
    {
        [SetUp]
        public void SetUp()
        {
            Fixture = new Fixture();
            Given();
            When();
        }

        protected Fixture Fixture { get; set; }

        public abstract void Given();
        public abstract void When();
    }
}