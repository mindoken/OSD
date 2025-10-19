using System;
using Infrastructure;

namespace CoreGame.Components
{
    [Serializable]
    public struct UnityViewModel
    {
        public MemoryPool<UnityView> MemoryPool;
        public UnityView CurrentView;
    }
}