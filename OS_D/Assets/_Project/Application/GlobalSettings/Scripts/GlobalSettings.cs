using System.Collections.Generic;

namespace Application
{
    public sealed class GlobalSettings
    {
        public IReadOnlyDictionary<SettingName, BoolSetting> BoolSettings => _boolSettings;
        private readonly Dictionary<SettingName, BoolSetting> _boolSettings = new();

        public IReadOnlyDictionary<SettingName, FloatSetting> FloatSettings => _floatSettings;
        private readonly Dictionary<SettingName, FloatSetting> _floatSettings = new();

        public IReadOnlyDictionary<SettingName, EnumSetting> EnumSettings => _enumSettings;
        private readonly Dictionary<SettingName, EnumSetting> _enumSettings = new();

        public GlobalSettings(GlobalSettingsPipeline pipeline)
        {
            for (int i = 0; i < pipeline.FloatSettings.Length; i++)
            {
                var setting = pipeline.FloatSettings[i];
                setting.Value.Value = setting.InitValue;
                _floatSettings.Add(setting.Name, setting);
            }
            for (int i = 0; i < pipeline.BoolSettings.Length; i++)
            {
                var setting = pipeline.BoolSettings[i];
                setting.Value.Value = setting.InitValue;
                _boolSettings.Add(setting.Name, setting);
            }
            for (int i = 0; i < pipeline.EnumSettings.Length; i++)
            {
                var setting = pipeline.EnumSettings[i];
                setting.Value.Value = setting.InitValue;
                _enumSettings.Add(setting.Name, setting);
            }
        }

        public BoolSetting GetBoolSetting(SettingName name)
        {
            return _boolSettings[name];
        }

        public bool TryGetBoolSetting(SettingName name, out BoolSetting setting)
        {
            return _boolSettings.TryGetValue(name, out setting);
        }

        public FloatSetting GetFloatSetting(SettingName name)
        {
            return _floatSettings[name];
        }

        public bool TryGetFloatSetting(SettingName name, out FloatSetting setting)
        {
            return _floatSettings.TryGetValue(name, out setting);
        }

        public EnumSetting GetEnumSetting(SettingName name)
        {
            return _enumSettings[name];
        }

        public bool TryGetEnumSetting(SettingName name, out EnumSetting setting)
        {
            return _enumSettings.TryGetValue(name, out setting);
        }
    }
}