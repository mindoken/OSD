namespace Infrastructure
{
    public interface ILoadCommand
    {
        void ExecuteStart();
        void ExecuteFinish();
    }
}