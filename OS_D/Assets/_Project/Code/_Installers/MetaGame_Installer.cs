using Alchemy.Inspector;
using App;
using UnityEngine;
using Zenject;

namespace MetaGame
{
    public sealed class MetaGame_Installer : MonoInstaller
    {
        [Header("Currency Bank")][SerializeField][LabelText("Pipeline")]
        private CurrencyBank_Pipeline _currencyBankPipeline;

        [Header("Value Data Services")][SerializeField][LabelText("Pipeline")]
        private ValueDataServices_Pipeline _valueDataServicesPipeline;

        [Header("Upgrades Manager")][SerializeField][LabelText("Pipeline")]
        private UpgradesManager_Pipeline _upgradesManagerPipeline;

        public override void InstallBindings()
        {
            InstallMeta();

            InstallControllers();

            InstallSaveLoaders();
        }

        private void InstallMeta()
        {
            Container
                .Bind<CurrencyBank>()
                .AsSingle()
                .WithArguments(_currencyBankPipeline);

            Container
                .Bind<ValueDataServices>()
                .AsSingle()
                .WithArguments(_valueDataServicesPipeline);

            Container
                .Bind<UpgradesManager>()
                .AsSingle()
                .WithArguments(_upgradesManagerPipeline);
        }

        private void InstallControllers()
        {

        }

        private void InstallSaveLoaders()
        {
            Container
                .Bind<ISaveLoader>()
                .To<CurrencyBank_SaveLoader>()
                .AsSingle();

            Container
                .Bind<ISaveLoader>()
                .To<ValueDataServices_SaveLoader>()
                .AsSingle();

            Container
                .Bind<ISaveLoader>()
                .To<UpgradesManager_SaveLoader>()
                .AsSingle();
        }
    }
}