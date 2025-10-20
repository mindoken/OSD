using UnityEngine;
using Zenject;

namespace App
{
    public sealed class Bootstrap_Installer : MonoInstaller
    {
        [SerializeField] private BootstrapScreen _view;

        public override void InstallBindings()
        {
            this.Container
                .Bind<IInitializable>()
                .To<Bootstrap>()
                .AsSingle()
                .WithArguments(_view);

            InstallLoadTasks();
        }

        private void InstallLoadTasks()
        {
            InstallLoadTask<LoadSaveRepository_LoadTask>();

            InstallLoadTask<LoadGameAssets_LoadTask>();
        }

        private void InstallLoadTask<T>()
            where T : ILoadTask
        {
            this.Container
                .Bind<ILoadTask>()
                .To<T>()
                .AsSingle();
        }
    }
}