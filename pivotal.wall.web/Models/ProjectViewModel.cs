using System;
using System.Collections.Generic;

namespace pivotal.wall.web.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
            Columns = new List<ColumnViewModel>();
        }

        public string Name { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }

        public IList<ColumnViewModel> Columns { get; set; }
    }

    public class ColumnViewModel
    {
        public IEnumerable<StoryViewModel> Stories { get; set; }
    }

    public class StoryViewModel
    {
        public string Title { get; set; }

        public string Points { get; set; }

        public string State { get; set; }
    }
}