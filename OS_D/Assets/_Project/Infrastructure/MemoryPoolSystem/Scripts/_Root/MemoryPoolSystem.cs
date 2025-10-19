using System.Collections.Generic;

namespace Infrastructure
{
    public sealed class MemoryPoolSystem : IMemoryPoolSystem
    {
        private readonly Dictionary<MemoryPoolName, IMemoryPool> _memoryPools = new();

        public MemoryPoolSystem(
            IMemoryPool[] memoryPools)
        {
            for (int i = 0; i < memoryPools.Length; i++)
            {
                var pool = memoryPools[i];
                pool.Init(); //temporary
                _memoryPools.Add(pool.Name, pool);
            }
        }

        void IMemoryPoolSystem.LoadPools()
        {
            foreach (var pool in _memoryPools.Values)
            {
                pool.Load();
            }
        }

        void IMemoryPoolSystem.InitializePools()
        {
            foreach (var pool in _memoryPools.Values)
            {
                pool.Init();
            }
        }

        MemoryPool<T> IMemoryPoolSystem.GetPool<T>(MemoryPoolName name)
        {
            return (MemoryPool<T>)_memoryPools[name];
        }
    }
}