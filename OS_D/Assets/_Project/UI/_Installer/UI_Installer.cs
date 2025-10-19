using Alchemy.Inspector;
using Common;
using Infrastructure;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace UI
{
    public sealed class UI_Installer : MonoInstaller
    {
        [Title("Currency Bank Widget")][ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,ShowFoldoutHeader = false,Reorderable = true)] [SerializeField]
        private CurrencyBankWidget_Pipeline[] _currencyBankWidgetPipeline;

        [Header("Global Settings Panel")][SerializeField][LabelText("Pipeline")] 
        private GlobalSettingsPanel_Pipeline _globalSettingsPanelPipeline;

        public override void InstallBindings()
        {
            InstallWidget<CurrencyBankShower, CurrencyBankWidget_Pipeline>(_currencyBankWidgetPipeline);
           
            //InstallWidget<GlobalSettingsPanelShower, GlobalSettingsPanel_Pipeline>(_globalSettingsPanelPipeline);
        }

        private void InstallWidget<TWidgetShower, TPipeline>(TPipeline[] pipelines)
        {
            for (int i = 0; i < pipelines.Length; i++)
            {
                Container
                    .Bind(typeof(IWidgetShower), typeof(IInitializable), typeof(IDisposable))
                    .To<TWidgetShower>()
                    .AsCached()
                    .WithArguments(pipelines[i]);
            }

            Container
                .BindExecutionOrder<TWidgetShower>(ExecutionOrders.INTERFACE);
        }

        private void InstallWidget<TWidgetShower, TPipeline>(TPipeline pipeline)
        {
            Container
                .Bind(typeof(IWidgetShower), typeof(IInitializable), typeof(IDisposable))
                .To<TWidgetShower>()
                .AsSingle()
                .WithArguments(pipeline);

            Container
                .BindExecutionOrder<TWidgetShower>(ExecutionOrders.INTERFACE);
        }

        private void InstallPopup<TPopupShower, TPopup>(TPopup popup, PopupName popupName)
        {
            Container
                .Bind(typeof(IPopupShower), typeof(IInitializable))
                .To<TPopupShower>()
                .AsSingle()
                .WithArguments(popupName, popup);
        }
    }
}