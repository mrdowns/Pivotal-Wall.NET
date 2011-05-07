namespace pivotal.wall.model
{
    public interface IProjectRepository
    {
        PivotalProject GetProject(int id);
    }
}