using App;
using Infrastructure;
using System.Collections.Generic;

namespace UI
{
    public sealed class GlobalSettingsPanelPresenter : IGlobalSettingsPanelPresenter
    {
        public IReadOnlyList<SettingGroupPresenter> SettingGroups => _settingGroups;
        private readonly List<SettingGroupPresenter> _settingGroups = new();

        public GlobalSettingsPanelPresenter(
            SettingGroupConfig[] settingGroups,
            ILocalization locale,
            GlobalSettings globalSettings,
            ScreenResolutionService resolutionService)
        {
            for (int i = 0; i < settingGroups.Length; i++)
            {
                ref var group = ref settingGroups[i];
                var groupPresenter = new SettingGroupPresenter();
                groupPresenter.TitlePresenter = new SimpleTitlePresenter(locale, group.GroupTitleKey);

                ref readonly var settings = ref group.Settings;
                for (int j = 0; j < settings.Length; j++)
                {
                    ref var setting = ref settings[j];
                    switch (setting.Type)
                    {
                        case SettingType.Bool:
                            groupPresenter.SettingPresenters.Add(new BoolSettingPresenter(locale, globalSettings.GetBoolSetting(setting.Setting)));
                            break;
                        case SettingType.Float:
                            groupPresenter.SettingPresenters.Add(new FloatSettingPresenter(locale, globalSettings.GetFloatSetting(setting.Setting)));
                            break;
                        case SettingType.Enum:
                            if (setting.Setting == SettingName.ScreenResolution)
                                groupPresenter.SettingPresenters.Add(new ScreenResolutionSettingPresenter(resolutionService, locale, globalSettings.GetEnumSetting(setting.Setting)));
                            else
                                groupPresenter.SettingPresenters.Add(new EnumSettingPresenter(locale, globalSettings.GetEnumSetting(setting.Setting)));
                            break;
                    }
                }

                _settingGroups.Add(groupPresenter);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < _settingGroups.Count; i++)
            {
                var settingGroup = _settingGroups[i];
                settingGroup.TitlePresenter.Dispose();
                for (int j = 0; j < settingGroup.SettingPresenters.Count; j++)
                {
                    settingGroup.SettingPresenters[j].Dispose();
                }
            }
        }
    }
}