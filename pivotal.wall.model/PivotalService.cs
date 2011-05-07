namespace pivotal.wall.model
{
    public class PivotalService
    {
        private readonly IProjectRepository _projectRepository;

        public PivotalService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public virtual PivotalProject GetProject(int id)
        {
            return _projectRepository.GetProject(id);
        }
    }
}