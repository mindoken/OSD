using Infrastructure;
using Leopotam.EcsLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
    public sealed class EcsEngine : IEcsEngine, IDisposable
    {
        public IReadOnlyDictionary<string, EcsWorld> Worlds => _worlds;
        private readonly Dictionary<string, EcsWorld> _worlds = new();

        public string DefaultWorld => _defaultWorld;
        private readonly string _defaultWorld;

        public EcsEngine()
        {
            var worldsCount = EcsWorlds.WORLDS.Length;
            _defaultWorld = EcsWorlds.WORLDS[0];

            for (var i = 0; i < worldsCount; i++)
            {
                _worlds.Add(EcsWorlds.WORLDS[i], new EcsWorld());
            }
        }

        public EcsWorld GetWorld(string worldName) => _worlds[worldName];

        void IDisposable.Dispose()
        {
            foreach (var pair in _worlds)
            {
                pair.Value.Destroy();
            }
            _worlds.Clear();
        }
    }
}