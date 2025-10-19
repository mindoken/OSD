using Application;
using Infrastructure;

namespace UI
{
    public sealed class GlobalSettingsPanelShower : WidgetShower<IGlobalSettingsPanelPresenter, GlobalSettingsPanel>
    {
        private readonly SettingGroupConfig[] _settingGroups;
        private readonly ILocalization _locale;
        private readonly ScreenResolutionService _resolutionService;
        private readonly GlobalSettings _globalSettings;

        public GlobalSettingsPanelShower(
            GlobalSettingsPanel_Pipeline pipeline,
            ILocalization locale,
            ScreenResolutionService resolutionService,
            GlobalSettings globalSettings) : base(pipeline.Prefab, pipeline.Name, pipeline.ShowOnStart)
        {
            _settingGroups = pipeline.Groups;
            _locale = locale;
            _resolutionService = resolutionService;
            _globalSettings = globalSettings;
        }

        protected override IGlobalSettingsPanelPresenter CreatePresenter()
        {
            return new GlobalSettingsPanelPresenter(_settingGroups, _locale, _globalSettings, _resolutionService);
        }
    }
}