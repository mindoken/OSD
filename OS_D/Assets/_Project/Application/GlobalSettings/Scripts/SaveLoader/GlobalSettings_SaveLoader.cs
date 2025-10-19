using Infrastructure;

namespace Application
{
    public sealed class GlobalSettings_SaveLoader : SaveLoader<GlobalSettings, GlobalSettings_SaveData>
    {
        private readonly GlobalSettings_SaveData _saveData = new();

        protected override GlobalSettings_SaveData ConvertToData(GlobalSettings service)
        {
            _saveData.ResetCachedData();
            
            using (var enumerator = service.BoolSettings.GetEnumerator())
            {
                while(enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    _saveData.BoolSettings.Add(current.Key, current.Value.Value.Value);
                }
            }
            using (var enumerator = service.FloatSettings.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    _saveData.FloatSettings.Add(current.Key, current.Value.Value.Value);
                }
            }
            using (var enumerator = service.EnumSettings.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    _saveData.EnumSettings.Add(current.Key, current.Value.Value.Value);
                }
            }
            return _saveData;
        }

        protected override void SetupData(GlobalSettings service, GlobalSettings_SaveData data)
        {
            for (int i = 0; i < data.BoolSettings.Size; i++)
            {
                var saveData = data.BoolSettings.CachedList[i];
                if (service.TryGetBoolSetting(saveData.SettingName, out var setting))
                    setting.Value.Value = saveData.Value;
            }
            for (int i = 0; i < data.EnumSettings.Size; i++)
            {
                var saveData = data.EnumSettings.CachedList[i];
                if (service.TryGetEnumSetting(saveData.SettingName, out var setting))
                    setting.Value.Value = saveData.Value;
            }
            for (int i = 0; i < data.FloatSettings.Size; i++)
            {
                var saveData = data.FloatSettings.CachedList[i];
                if (service.TryGetFloatSetting(saveData.SettingName, out var setting))
                    setting.Value.Value = saveData.Value;
            }
        }
    }
}