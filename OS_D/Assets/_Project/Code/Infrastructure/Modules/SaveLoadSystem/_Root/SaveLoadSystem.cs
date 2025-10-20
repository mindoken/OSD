using Cysharp.Threading.Tasks;
using Zenject;
using UnityEngine;
using Infrastructure;

namespace App
{
    public sealed class SaveLoadSystem : ISaveLoadSystem
    {
        private readonly ISaveLoader[] _applicationSaveLoaders;
        private readonly ISaveLoader[] _gameplaySaveLoaders;

        private readonly ISaveRepositorySystem _saveRepositorySystem;

        private readonly ISaveCommand _saveCommand;
        private readonly ILoadCommand _loadCommand;

        public SaveLoadSystem(
            [Inject(Optional = true, Source = InjectSources.Local)] ISaveLoader[] gameplaySaveLoaders,
            [Inject(Optional = true, Source = InjectSources.Parent)] ISaveLoader[] applicationSaveLoaders,
            ISaveRepositorySystem saveRepositorySystem,
            [Inject(Optional = true)] ISaveCommand saveCommand,
            [Inject(Optional = true)] ILoadCommand loadCommand)
        {
            _applicationSaveLoaders = applicationSaveLoaders;
            _gameplaySaveLoaders = gameplaySaveLoaders;
            _saveRepositorySystem = saveRepositorySystem;
            _saveCommand = saveCommand;
            _loadCommand = loadCommand;
        }

        public async UniTask Save()
        {
            _saveCommand?.ExecuteStart();

            var applicationRepository = _saveRepositorySystem.ApplicationRepository;
            var gameplayRepository = _saveRepositorySystem.GameplayRepository;

            if (gameplayRepository.CurrentKey == string.Empty)
                return;

            for (int i = 0; i < _applicationSaveLoaders.Length; i++)
            {
                var saveloader = _applicationSaveLoaders[i];
                saveloader.SaveData(applicationRepository);
            }

            for (int i = 0; i < _gameplaySaveLoaders.Length; i++)
            {
                var saveloader = _gameplaySaveLoaders[i];
                saveloader.SaveData(gameplayRepository);
            }

            await _saveRepositorySystem.SaveRepositories();

            _saveCommand?.ExecuteFinish();
        }

        public void Load()
        {
            _loadCommand?.ExecuteStart();

            var applicationRepository = _saveRepositorySystem.ApplicationRepository;
            var gameplayRepository = _saveRepositorySystem.GameplayRepository;

            for (int i = 0; i < _applicationSaveLoaders.Length; i++)
            {
                var saveloader = _applicationSaveLoaders[i];
                saveloader.LoadData(applicationRepository);
            }

            for (int i = 0; i < _gameplaySaveLoaders.Length; i++)
            {
                var saveloader = _gameplaySaveLoaders[i];
                saveloader.LoadData(gameplayRepository);
            }

            _loadCommand?.ExecuteFinish();

            Debug.Log($"<color=green>Application data loaded from Key {applicationRepository.CurrentKey}!</color>");
            Debug.Log($"<color=green>Gameplay data loaded from Key {gameplayRepository.CurrentKey}!</color>");
        }
    }
}