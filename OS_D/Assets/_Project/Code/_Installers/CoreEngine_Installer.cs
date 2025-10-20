using Leopotam.EcsLite;
using Zenject;

namespace CoreGame
{
    public sealed class CoreEngine_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUpdateSystems();
            BindFixedUpdateSystems();
            BindLateUpdateSystems();
        }

        private void BindUpdateSystems()
        {
            const string ID = "Default";
            ///-------------INSTALL MODEL--------------


            ///-------------MODEL PROCESS--------------


        }

        private void BindFixedUpdateSystems()
        {
            //const string ID = "Fixed";
        }

        private void BindLateUpdateSystems()
        {
            const string ID = "Late";
            ///--------------INSTALL VIEW--------------
            BindSystem<AllView_InstallSystem>(ID);



            ///--------------VIEW PROCESS--------------
            BindSystem<UnityTransformWithPosition_SynchronizationSystem>(ID);


            ///-------------UNINSTALL VIEW--------------


            BindSystem<AllView_UninstallSystem>(ID);

            ///-------------UNINSTALL MODEL-------------


            ///------------------DISPOSE----------------
            BindSystem<AllUninstall_DisposeSystem>(ID);
        }

        private void BindSystem<TSystem>(string id)
            where TSystem : IEcsSystem
        {
            Container
                .Bind<IEcsSystem>()
                .WithId(id)
                .To<TSystem>()
                .AsSingle();
        }
    }
}
