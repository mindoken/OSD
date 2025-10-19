using App;
using Zenject;

namespace Infrastructure
{
    public sealed class SceneLoader : IInitializable
    {
        private readonly ISaveLoadSystem _saveLoadSystem;

        public SceneLoader(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
        }

        void IInitializable.Initialize()
        {
            _saveLoadSystem.Load();
        }
    }
}