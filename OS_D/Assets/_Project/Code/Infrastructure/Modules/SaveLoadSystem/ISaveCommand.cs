namespace Infrastructure
{
    public interface ISaveCommand
    {
        void ExecuteStart();
        void ExecuteFinish();
    }
}