using Alchemy.Inspector;
using Application.SaveRepository;
using DG.Tweening;
using Infrastructure;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Application
{
    public sealed class ProjectContext_Installer : MonoInstaller
    {
        [Header("Cursor Service")]
        [SerializeField] private Texture2D _defaultCursor;

        [Header("Global Settings")]
        [SerializeField] private GlobalSettingsPipeline _globalSettingsPipeline;

        [Space(10f)]
        [Title("Sprite Atlases")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All, ShowFoldoutHeader = false, Reorderable = true)]
        [SerializeField] private SpriteAtlasInfo[] _spriteAtlases;

        public override void InstallBindings()
        {
            DOTween.SetTweensCapacity(500, 50);

            InstallSystems();

            InstallServices();

            InstallSaveLoaders();

            InstallCommands();
        }

        private void InstallSystems()
        {
            this.Container
                .Bind<ISaveRepositorySystem>()
                .To<SaveRepositorySystem>()
                .AsSingle();

            this.Container
                .Bind<GlobalSettings>()
                .AsSingle()
                .WithArguments(_globalSettingsPipeline);

            this.Container
                .Bind(typeof(ILocalization), typeof(IDisposable))
                .To<UnityLocalizationSystem>()
                .AsSingle();
        }

        private void InstallServices()
        {
            this.Container
                .Bind<CursorService>()
                .AsSingle()
                .WithArguments(_defaultCursor);

            this.Container
                .Bind<ScreenResolutionService>()
                .AsSingle()
                .NonLazy();

            this.Container
                .Bind<SpriteAtlasService>()
                .AsSingle()
                .WithArguments(_spriteAtlases);

            this.Container
                .Bind<SceneManagerService>()
                .AsSingle();
        }

        private void InstallCommands()
        {
            this.Container
                .Bind<ISceneLoadStartCommand>()
                .To<SceneLoadStartCommand>()
                .AsSingle();
        }

        private void InstallSaveLoaders()
        {
            this.Container
                .Bind<ISaveLoader>()
                .To<GlobalSettings_SaveLoader>()
                .AsSingle();
        }
    }
}