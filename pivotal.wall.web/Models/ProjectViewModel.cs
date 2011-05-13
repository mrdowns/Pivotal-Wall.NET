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
            foreach (var column in columns)
            {
                var stories = column.GetStoriesFor(project);
                Columns.Add(new ColumnViewModel(column, stories));
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
            Title = column.State ?? column.Label;
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