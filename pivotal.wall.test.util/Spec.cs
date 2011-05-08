using NUnit.Framework;
using Ploeh.AutoFixture;

namespace pivotal.wall.test.util
{
    public abstract class Spec
    {
        [SetUp]
        public void SetUp()
        {
            Fixture = new Fixture().Customize(new MultipleCustomization());
            Given();
            When();
        }

        protected IFixture Fixture { get; set; }

        public abstract void Given();
        public abstract void When();
    }
}