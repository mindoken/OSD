using UnityEngine;
using Infrastructure;

namespace Common
{
    public class PoolItemDestroyCommand<T> : IDestroyCommand<T>
        where T : Component
    {
        private readonly MemoryPool<T> _memoryPool;

        public PoolItemDestroyCommand(MemoryPool<T> memoryPool)
        {
            _memoryPool = memoryPool;
        }

        public virtual void Execute(T destroyedItem, bool culled = false)
        {
            _memoryPool.UnspawnItem(destroyedItem);
        }
    }
}