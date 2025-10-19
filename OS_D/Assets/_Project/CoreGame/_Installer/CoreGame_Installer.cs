using App;
using Common;
using Infrastructure;
using UnityEngine;
using Zenject;
using IMemoryPool = Infrastructure.IMemoryPool;

namespace CoreGame
{
    public sealed class CoreGame_Installer : MonoInstaller
    {
        [SerializeField] private MemoryPoolInfo<UnityView> _weaponPoolInfo;

        [SerializeField] private MemoryPoolInfo<UnityView> _projectilePoolInfo;

        public override void InstallBindings()
        {
            InstallSystems();

            InstallCommands();

            InstallControllers();
        }

        private void InstallSystems()
        {
        }

        private void InstallCommands()
        {
        }

        private void InstallControllers()
        {
        }

        private void BindMemoryPool<T>(MemoryPoolInfo<T> info)
            where T : Component
        {
            Container
                .Bind<IMemoryPool>()
                .To<Infrastructure.MemoryPool<T>>()
                .AsCached()
                .WithArguments(info);
        }
    }
}
