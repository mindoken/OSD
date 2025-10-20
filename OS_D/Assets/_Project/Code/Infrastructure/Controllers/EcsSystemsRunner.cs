using App;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Collections.Generic;
using Zenject;

namespace Infrastructure
{
    public sealed class EcsSystemsRunner :
        IDisposable,
        IGameTickable,
        IGameFixedTickable,
        ITickable,
        IFixedTickable,
        IGameLateTickable,
        ILateTickable
    {
        private readonly IEcsSystems _update;
        private readonly IEcsSystems _fixedUpdate;
        private readonly IEcsSystems _lateUpdate;

        private readonly IEcsSystems _gameUpdate;
        private readonly IEcsSystems _gameFixedUpdate;
        private readonly IEcsSystems _gameLateUpdate;

        private readonly IEcsSystem[] _gameDefaultSystems;
        private readonly IEcsSystem[] _gameFixedSystems;
        private readonly IEcsSystem[] _gameLateSystems;

        private readonly IEcsSystem[] _defaultSystems;
        private readonly IEcsSystem[] _fixedSystems;
        private readonly IEcsSystem[] _lateSystems;

        public EcsSystemsRunner(
            [Inject(Id = "GameDefault", Optional = true)] IEcsSystem[] gameDefaultSystems,
            [Inject(Id = "GameFixed", Optional = true)] IEcsSystem[] gameFixedSystems,
            [Inject(Id = "GameLate", Optional = true)] IEcsSystem[] gameLateSystems,
            [Inject(Id = "Default", Optional = true)] IEcsSystem[] defaultSystems,
            [Inject(Id = "Fixed", Optional = true)] IEcsSystem[] fixedSystems,
            [Inject(Id = "Late", Optional = true)] IEcsSystem[] lateSystems,
            IEcsEngine ecsEngine)
        {
            _gameDefaultSystems = gameDefaultSystems;
            _gameFixedSystems = gameFixedSystems;
            _gameLateSystems = gameLateSystems;
            _defaultSystems = defaultSystems;
            _fixedSystems = fixedSystems;
            _lateSystems = lateSystems;

            var worlds = ecsEngine.Worlds;
            var defaultWorld = ecsEngine.DefaultWorld;

            _update = InitializeSystems(_defaultSystems, worlds, defaultWorld, false, "Update");
            _fixedUpdate = InitializeSystems(_fixedSystems, worlds, defaultWorld, false, "Fixed Update");
            _lateUpdate = InitializeSystems(_lateSystems, worlds, defaultWorld, true, "Late Update");

            _gameUpdate = InitializeSystems(_gameDefaultSystems, worlds, defaultWorld, false, "Game Update");
            _gameFixedUpdate = InitializeSystems(_gameFixedSystems, worlds, defaultWorld, false, "Game Fixed Update");
            _gameLateUpdate = InitializeSystems(_gameLateSystems, worlds, defaultWorld, false, "Game Late Update");
        }

        private IEcsSystems InitializeSystems(
            IEcsSystem[] systems,
            IReadOnlyDictionary<string, EcsWorld> worlds,
            string defaultWorld,
            bool isDebug = false,
            string systemsName = "")
        {
            var target = new EcsSystems(worlds[defaultWorld]);

            foreach (var pair in worlds)
            {
                target.AddWorld(pair.Value, pair.Key);
            }

            for (var i = 0; i < systems.Length; i++)
            {
                target.Add(systems[i]);
            }

#if UNITY_EDITOR
            target.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem(systemsName));

            if (isDebug)
            {
                target.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
                foreach (var key in worlds.Keys)
                {
                    if (key == defaultWorld)
                        continue;
                    target.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(key));
                }
            }
#endif
            target.Inject();
            target.Init();
            return target;
        }

        void IDisposable.Dispose()
        {
            DisposeSystems(_update);
            DisposeSystems(_fixedUpdate);
            DisposeSystems(_lateUpdate);

            DisposeSystems(_gameUpdate);
            DisposeSystems(_gameFixedUpdate);
            DisposeSystems(_gameLateUpdate);
        }

        private void DisposeSystems(IEcsSystems systems)
        {
            if (systems != null)
            {
                systems.Destroy();
                systems = null;
            }
        }

        void IGameFixedTickable.FixedTick(float _)
        {
            _gameFixedUpdate?.Run();
        }

        void IFixedTickable.FixedTick()
        {
            _fixedUpdate?.Run();
        }

        void IGameTickable.Tick(float _)
        {
            _gameUpdate?.Run();
        }

        void ITickable.Tick()
        {
            _update?.Run();
        }

        void IGameLateTickable.LateTick(float _)
        {
            _gameLateUpdate?.Run();
        }

        void ILateTickable.LateTick()
        {
            _lateUpdate?.Run();
        }
    }
}