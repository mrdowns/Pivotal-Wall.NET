using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using pivotal.wall.model;
using pivotal.wall.web.Helpers;

namespace pivotal.wall.web.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(Project project, IEnumerable<Column> columns)
        {
            Name = project.Name;

            Columns = new List<ColumnViewModel>();
            var stories = project.Stories;
            foreach (var column in columns.Reverse())
            {
                var matchingStories = column.FilterStories(stories);

                Columns.Insert(0, new ColumnViewModel(column, matchingStories));

                stories = stories.Except(matchingStories);
            }
        }

        public string Name { get; private set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }

        public IList<ColumnViewModel> Columns { get; set; }
    }

    public class ColumnViewModel
    {
        public ColumnViewModel(Column column, IEnumerable<Story> stories)
        {
            Title = string.Join(", ", column.Labels.Select(s => s).Union(column.States.Select(s => s)));
            Stories = Mapper.Map<IEnumerable<Story>, IEnumerable<StoryViewModel>>(stories);
        }

        public IEnumerable<StoryViewModel> Stories { get; set; }

        public string Title { get; set; }
    }

    public class StoryViewModel
    {
        public string Title { get; set; }

        public string Points { get; set; }

        public string State { get; set; }
    }
}