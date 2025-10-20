using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Infrastructure
{
    public interface IEcsEngine
    {
        IReadOnlyDictionary<string, EcsWorld> Worlds { get; }
        public string DefaultWorld { get; }
        EcsWorld GetWorld(string worldName);
    }
}