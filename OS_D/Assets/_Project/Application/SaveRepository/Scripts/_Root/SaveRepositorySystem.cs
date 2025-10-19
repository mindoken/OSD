using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Application.SaveRepository
{
    public sealed class SaveRepositorySystem : ISaveRepositorySystem
    {
        private readonly string[] APP_SAVE_KEY = { "application" };
        private readonly string[] GAME_SAVE_KEYS = { "profile1", "profile2", "profile3", "profile4" };

        public ISaveRepository ApplicationRepository => _applicationRepository;
        private readonly ISaveRepository _applicationRepository;
        public ISaveRepository GameplayRepository => _gameplayRepository;
        private readonly ISaveRepository _gameplayRepository;
        public string CurrentSaveProfile => _gameplayRepository.CurrentKey;

        private readonly List<UniTask> _taskCache = new();
        private readonly CurrentProfileKey _profileCache = new();

        private bool _isSaving = false;

        public SaveRepositorySystem()
        {
            //var saveStrategy = new PlayerPrefsSaveStrategy();
            var saveStartegy = new AppDataSaveStrategy();
            _applicationRepository = new SaveRepository(SaveRepositoryName.Application, saveStartegy);
            _gameplayRepository = new SaveRepository(SaveRepositoryName.Gameplay, saveStartegy);
        }

        public async UniTask LoadRepositories()
        {
            _taskCache.Clear();
            _taskCache.Add(_applicationRepository.Load(APP_SAVE_KEY));
            _taskCache.Add(_gameplayRepository.Load(GAME_SAVE_KEYS));
            await UniTask.WhenAll(_taskCache);
            _applicationRepository.SetCurrentSaveKey(APP_SAVE_KEY[0]);

            if (_applicationRepository.TryGetData<CurrentProfileKey>(out var key))
            {
                var profile = key.Key;
                if (profile != string.Empty)
                {
                    _gameplayRepository.SetCurrentSaveKey(profile);
                }
            }
        }

        public async UniTask SaveRepositories()
        {
            if (_isSaving)
                return;
            _isSaving = true;
            _profileCache.Key = _gameplayRepository.CurrentKey;
            _applicationRepository.SetData<CurrentProfileKey>(_profileCache);

            _taskCache.Clear();
            _taskCache.Add(_gameplayRepository.SaveCurrent());
            _taskCache.Add(_applicationRepository.SaveCurrent());
            await UniTask.WhenAll(_taskCache);
            _isSaving = false;
        }
    }

    public sealed class CurrentProfileKey
    {
        public string Key;
    }
}