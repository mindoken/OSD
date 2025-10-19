using UnityEngine;
using Zenject;

namespace Application
{
    public sealed class AppLoader_Installer : MonoInstaller
    {
        [SerializeField] private AppLoaderView _view;
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesTo<AppLoader>()
                .AsSingle()
                .WithArguments(_view);
        }
    }
}