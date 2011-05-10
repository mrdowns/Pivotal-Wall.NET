using System.Collections.Generic;

namespace pivotal.wall.model
{
    public interface IProjectRepository
    {
        Project GetProject(int id);
        IEnumerable<Story> GetStoriesForProject(int projectId);
        IEnumerable<Story> GetStoriesByFilter(int projectId, string filter);
    }
}