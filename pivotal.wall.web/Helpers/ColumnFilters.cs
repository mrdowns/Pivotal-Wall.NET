using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        
    }
}