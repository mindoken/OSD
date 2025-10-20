using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Zenject;

namespace App
{
    public sealed class Bootstrap :
        IInitializable
    {
        private const int NEXT_SCENE_BUILD_IDX = 1;

        private readonly List<ILoadTask> _loadTasks = new();
        private readonly BootstrapScreen _view;
        private readonly SceneManagerService _sceneManager;

        public Bootstrap(
            BootstrapScreen view,
            [InjectOptional] List<ILoadTask> loadTasks,
            SceneManagerService sceneManager)
        {
            _view = view;
            _loadTasks = loadTasks;
            _sceneManager = sceneManager;
        }

        void IInitializable.Initialize()
        {
            StartAllLoadTasks().Forget();
        }

        public async UniTaskVoid StartAllLoadTasks()
        {
            int countTasks = _loadTasks.Count;
            _view.SetSliderMaxValue(countTasks);
            for (var index = 0; index < countTasks; index++)
            {
                ILoadTask loadTask = _loadTasks[index];
                _view.SetProgressTitle(loadTask.Title);
                float progress = index + 1;
                _view.SetSliderValue(progress);
                _view.SetProgress($"{progress / countTasks * 100:0.00}%");
                await loadTask.LoadAsync();
            }
            RunScene().Forget();
        }

        private async UniTaskVoid RunScene()
        {
            await _sceneManager.LoadSceneAsync(NEXT_SCENE_BUILD_IDX);
        }
    }
}