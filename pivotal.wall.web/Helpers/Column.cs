using System.Collections;
using System.Collections.Generic;
using System.Linq;
using pivotal.wall.model;

namespace pivotal.wall.web.Helpers
{
    public class Column
    {
        public Column()
        {
            Labels = new List<string>();
            States = new List<string>();
        }
        
        public IList<string> Labels { get; private set; }

        public IList<string> States { get; private set; }

        public IEnumerable<Story> FilterStories(IEnumerable<Story> stories)
        {
            var statesMatch = stories.Where(s => States.Contains(s.State.ToString()));
            
            var labelsMatch = stories.Where(s => s.Labels != null && Labels.Intersect(s.Labels).Any());

            return statesMatch.Union(labelsMatch);

            //return stories.Where(s => States.Contains(s.State.ToString()) || Labels.Union(s.Labels ?? new List<string>()).Any());
        }

        public IEnumerable<Story> GetStoriesFor(Project project)
        {
            return FilterStories(project.Stories);
        }
    }
}