using System;
using UnityEngine;

namespace Infrastructure
{
    [Serializable]
    public struct MemoryPoolInfo<T>
        where T : Component
    {
        public T Prefab;
        public MemoryPoolName Name;
        public TransformName TransformName;
        public int InitialCount;
    }
}