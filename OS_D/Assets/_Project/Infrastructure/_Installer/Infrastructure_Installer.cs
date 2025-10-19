using Alchemy.Inspector;
using App;
using Common;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Infrastructure
{
    public sealed class Infrastructure_Installer : MonoInstaller
    {
        [Header("Widget Manager")][LabelText("Pipeline")]
        [SerializeField] private WidgetManager_Pipeline _widgetManagerPipeline;

        [Space(10f)][Title("Transform Provider")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All, ShowFoldoutHeader = false, Reorderable = true)]
        [SerializeField] private TransformInfo[] _transformInfo;

        public override void InstallBindings()
        {
            InstallSystems();

            InstallControllers();
        }

        private void InstallSystems()
        {
            Container
                .Bind<GameCycle>()
                .AsSingle();

            Container
                .Bind<GameTickableManager>()
                .AsSingle();

            Container
                .Bind<IPopupManager>()
                .To<PopupManager>()
                .AsSingle();

            Container
                .Bind<IPrefabFactory>()
                .To<PrefabFactory>()
                .AsSingle();

            Container
                .Bind<ITransformProvider>()
                .To<TransformProvider>()
                .AsSingle()
                .WithArguments(_transformInfo);

            Container
                .Bind<ISaveLoadSystem>()
                .To<SaveLoadSystem>()
                .AsSingle();

            Container
                .Bind<IWidgetManager>()
                .To<WidgetManager>()
                .AsSingle()
                .WithArguments(_widgetManagerPipeline);

            Container
                .Bind(typeof(IEcsEngine), typeof(IDisposable))
                .To<EcsEngine>()
                .AsSingle();

            Container
                .Bind<IMemoryPoolSystem>()
                .To<MemoryPoolSystem>()
                .AsSingle();
        }

        private void InstallControllers()
        {
            Container
                .BindInterfacesTo<EcsSystemsRunner>()
                .AsSingle();

            Container
                .Bind(typeof(IFixedTickable), typeof(IDisposable))
                .To<AutoSaver>()
                .AsSingle();

            Container
                .Bind<IInitializable>()
                .To<SceneLoader>()
                .AsSingle();

            Container
                .BindExecutionOrder<SceneLoader>(ExecutionOrders.SAVELOADER);
        }
    }
}
