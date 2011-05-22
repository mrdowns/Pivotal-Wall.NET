using System.Collections.Generic;
using AutoMapper;
using pivotal.wall.model;
using PivotalTracker.FluentAPI.Repository;
using Project = pivotal.wall.model.Project;
using Story = pivotal.wall.model.Story;
using PivotalProject = PivotalTracker.FluentAPI.Domain.Project;
using PivotalStory = PivotalTracker.FluentAPI.Domain.Story;

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
            Mapper.CreateMap<PivotalProject, Project>();
            Mapper.CreateMap<PivotalStory, Story>()
                .ForMember(s => s.Title, s => s.MapFrom(st => st.Name))
                .ForMember(s => s.Points, s => s.MapFrom(st => st.Estimate))
                .ForMember(s => s.State, s => s.MapFrom(st => st.CurrentState))
                .ForMember(s => s.Labels, s => s.MapFrom(st => st.Labels))
                .ForMember(s => s.Owner, s => s.MapFrom(st => st.OwnedBy));

            _pivotalProjectRepository = pivotalProjectRepository;
            _pivotalStoryRepository = pivotalStoryRepository;
        }

        public Project GetProject(int id)
        {
            var project = _pivotalProjectRepository.GetProject(id);

            return Mapper.Map<PivotalProject, Project>(project);
        }

        public IEnumerable<Story> GetStoriesForProject(int id)
        {
            var stories = _pivotalStoryRepository.GetStories(id);
            
            return Mapper.Map<IEnumerable<PivotalStory>, IEnumerable<Story>>(stories);
        }

        public IEnumerable<Story> GetStoriesByFilter(int id, string filter)
        {
            var stories = _pivotalStoryRepository.GetSomeStories(id, filter);

            return Mapper.Map<IEnumerable<PivotalStory>, IEnumerable<Story>>(stories);
        }
    }
}
