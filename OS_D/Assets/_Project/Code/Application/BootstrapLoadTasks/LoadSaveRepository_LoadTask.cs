using Cysharp.Threading.Tasks;

namespace App
{
    public sealed class LoadSaveRepository_LoadTask : ILoadTask
    {
        string ILoadTask.Title => "Loading SaveRepository...";

        private readonly ISaveRepositorySystem _saveRepositorySystem;


        public LoadSaveRepository_LoadTask(
            ISaveRepositorySystem saveRepositorySystem)
        {
            _saveRepositorySystem = saveRepositorySystem;
        } 

        async UniTask ILoadTask.LoadAsync()
        {
            await _saveRepositorySystem.LoadRepositories();
        }
    }
}