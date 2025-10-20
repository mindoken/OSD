using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Zenject;


namespace App
{
    public sealed class LoadGameAssets_LoadTask : ILoadTask
    {
        private readonly List<IAssetLoader> _loaders;

        public LoadGameAssets_LoadTask([InjectOptional] List<IAssetLoader> loaders)
        {
            _loaders = loaders;
        }

        public string Title => "Load Game Configs...";

        public async UniTask LoadAsync()
        {
            var tasks = new List<UniTask>();
            foreach (var loader in _loaders)
            {
                tasks.Add(loader.LoadAsset());
            }
            await UniTask.WhenAll(tasks);
        }
    }
}