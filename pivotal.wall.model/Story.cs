using System;
using System.Collections.Generic;

namespace pivotal.wall.model
{
    public class Story
    {
        public virtual string Title { get; set; }

        public virtual int Points { get; set; }

        public virtual State State { get; set; }

        public List<string> Labels { get; set; }

        public string Owner { get; set; }
    }
}