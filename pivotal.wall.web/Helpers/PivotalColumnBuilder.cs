using System;
using System.Collections.Generic;

namespace pivotal.wall.web.Helpers
{
    public class PivotalColumnBuilder
    {
        private readonly string _columnParameters;

        public PivotalColumnBuilder(string columnParameters)
        {
            _columnParameters = columnParameters;
        }

        public virtual IList<Column> GetColumns()
        {
            var columns = new List<Column>();

            var terms = _columnParameters.Split(new[]{";"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var term in terms)
            {
                var matchers = term.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);

                var column = new Column();

                foreach (var matcher in matchers)
                {
                    AddTerm(matcher, column);
                }
                
                columns.Add(column);
            }

            return columns;
        }

        private void AddTerm(string term, Column column)
        {
            var spec = term.Split(new[] {"="}, StringSplitOptions.RemoveEmptyEntries);
            var type = spec[0].Trim();
            var value = spec[1].Trim();
            
            if (type.Equals("label"))
                column.Labels.Add(value);

            if (type.Equals("state"))
                column.States.Add(value);
        }
    }
}