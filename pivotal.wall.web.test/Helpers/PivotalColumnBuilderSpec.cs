using System;
using System.Collections.Generic;
using NUnit.Framework;
using pivotal.wall.model;
using pivotal.wall.test.util;
using pivotal.wall.web.Helpers;
using System.Linq;
using Should;

namespace pivotal.wall.web.test.Helpers
{
    [TestFixture]
    public class PivotalColumnBuilderSpec : Spec
    {
        private PivotalColumnBuilder _builder;
        private IList<Column> _columns;

        public override void Given()
        {
            _builder = new PivotalColumnBuilder("label=first label, state=Finished, state=Rejected, label=another label");
        }

        public override void When()
        {
            _columns = _builder.GetColumns();
        }

        [Test]
        public void should_return_columns_in_order()
        {
            _columns.ElementAt(0).Label.ShouldEqual("first label");
            _columns.ElementAt(0).State.ShouldBeNull();

            _columns.ElementAt(1).State.ShouldEqual(State.Finished.ToString());
            _columns.ElementAt(1).Label.ShouldBeNull();

            _columns.ElementAt(2).State.ShouldEqual(State.Rejected.ToString());
            _columns.ElementAt(2).Label.ShouldBeNull();

            _columns.ElementAt(3).Label.ShouldEqual("another label");
            _columns.ElementAt(3).State.ShouldBeNull();
        }
    }
}