namespace pivotal.wall.model
{
    public class PivotalService
    {
        public virtual PivotalProject GetProject(int id)
        {
            return new PivotalProject{Name="nothing"};
        }
    }
}