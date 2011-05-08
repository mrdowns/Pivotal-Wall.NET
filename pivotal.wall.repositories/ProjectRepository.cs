using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using pivotal.wall.model;
using PivotalTracker.FluentAPI.Domain;
using PivotalTracker.FluentAPI.Repository;
using Project = pivotal.wall.model.Project;
using Story = pivotal.wall.model.Story;

namespace pivotal.wall.repositories
{
    /// <summary>
    /// simply acts as a facade around the fluent api so we can test
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly PivotalProjectRepository _pivotalProjectRepository;
        private readonly PivotalStoryRepository _pivotalStoryRepository;

        public ProjectRepository(
            PivotalProjectRepository pivotalProjectRepository,
            PivotalStoryRepository pivotalStoryRepository)
        {
            Mapper.CreateMap<PivotalTracker.FluentAPI.Domain.Project, Project>();
            Mapper.CreateMap<PivotalTracker.FluentAPI.Domain.Story, Story>()
                .ForMember(s => s.Title, s => s.MapFrom(st => st.Name));

            _pivotalProjectRepository = pivotalProjectRepository;
            _pivotalStoryRepository = pivotalStoryRepository;
        }

        public Project GetProject(int id)
        {
            var project = _pivotalProjectRepository.GetProject(id);

            return Mapper.Map<PivotalTracker.FluentAPI.Domain.Project, Project>(project);
        }

        public IEnumerable<Story> GetStoriesForProject(int projectId)
        {
            var stories = _pivotalStoryRepository.GetStories(projectId);
            
            return Mapper.Map<IEnumerable<PivotalTracker.FluentAPI.Domain.Story>, IEnumerable<Story>>(stories);
        }
    }
}
