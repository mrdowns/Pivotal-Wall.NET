using System;
using System.Collections.Generic;

namespace pivotal.wall.web.Models
{
    public class ProjectViewModel
    {
        public string Name { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }
    }

    public class StoryViewModel
    {
        public string Title { get; set; }
    }
}