using System;
using System.Collections.Generic;

namespace pivotal.wall.model
{
    public class Project
    {
        public virtual string Name { get; set; }

        public virtual IEnumerable<Story> Stories { get; set; }
    }

    public class Story
    {
        public virtual string Title { get; set; }

        public virtual int Points { get; set; }
    }
}