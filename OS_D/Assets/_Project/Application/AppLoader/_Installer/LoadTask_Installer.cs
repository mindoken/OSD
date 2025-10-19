using Zenject;

namespace Application
{
    public sealed class LoadTask_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container
                .Bind<ILoadTask>()
                .To<LoadSaveRepository_LoadTask>()
                .AsSingle();

            this.Container
                .Bind<ILoadTask>()
                .To<LoadGameAssets_LoadTask>()
                .AsSingle();
        }
    }
}