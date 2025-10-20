namespace Infrastructure
{
    public interface IMemoryPool
    {
        MemoryPoolName Name { get; }
        void Load();
        void Init();
    }
}