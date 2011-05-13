using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using pivotal.wall.model;

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

            var terms = _columnParameters.Split(new[]{","}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var term in terms)
            {
                var spec = term.Split(new[] {"="}, StringSplitOptions.RemoveEmptyEntries);
                var type = spec[0].Trim();
                var value = spec[1].Trim();

                var column = new Column();

                if (type.Equals("label"))
                    column.Label = value;

                if (type.Equals("state"))
                    column.State = value;
                
                columns.Add(column);
            }

            return columns;
        }
    }

    public class Column
    {
        public string Label { get; set; }

        public string State { get; set; }

        public IEnumerable<Story> GetStoriesFor(Project project)
        {
            var c = this;
            return project.Stories.Where(
                s => s.State.ToString() == c.State 
                || (s.Labels != null && s.Labels.Any(l => l == c.Label)));
        }
    }
}