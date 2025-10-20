using UnityEngine;

namespace Infrastructure
{
    public interface IMemoryPoolSystem
    {
        void LoadPools();
        void InitializePools();
        MemoryPool<T> GetPool<T>(MemoryPoolName name) where T : Component;
    }
}