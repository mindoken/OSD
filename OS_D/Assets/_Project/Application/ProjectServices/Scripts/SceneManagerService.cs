using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using Application.SaveRepository;


namespace Application
{
    public sealed class SceneManagerService
    {
        private readonly ISaveRepositorySystem _saveRepositorySystem;
        private readonly ISceneLoadStartCommand _sceneLoadStartCommand;

        public SceneManagerService(
            ISaveRepositorySystem saveRepositorySystem,
            ISceneLoadStartCommand sceneLoadStartCommand)
        {
            _saveRepositorySystem = saveRepositorySystem;
            _sceneLoadStartCommand = sceneLoadStartCommand;
        }

        public async UniTask LoadSceneAsync(int NEXT_SCENE_BUILD_IDX)
        {
            _sceneLoadStartCommand.Execute();

            if (_saveRepositorySystem.CurrentSaveProfile == string.Empty)
            {
                _saveRepositorySystem.GameplayRepository.SetCurrentSaveKey("profile1");
            }

            await SceneManager.LoadSceneAsync(NEXT_SCENE_BUILD_IDX, LoadSceneMode.Additive);
            Debug.Log($"<color=green>Scene Loaded:</color> {SceneManager.GetSceneByBuildIndex(NEXT_SCENE_BUILD_IDX).name}");
            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(NEXT_SCENE_BUILD_IDX));
        }
    }
}