using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using pivotal.wall.model;

namespace pivotal.wall.web.Helpers
{
    public class PivotalColumnBuilder
    {
        public PivotalColumnBuilder(string columnParameters)
        {
            
        }

        public virtual IList<Column> GetColumns()
        {
            return new List<Column>();
        }
    }

    public class ColumnFilters : IEnumerable<Column>
    {
        protected ColumnFilters()
        {
            
        }

        public static ColumnFilters FromConfigString(string list)
        {
            return new ColumnFilters();
        }

        public string FilterString { get; set; }

        public IEnumerator<Column> GetEnumerator()
        {
            yield return new Column();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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