using Infrastructure;

namespace App
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

    public sealed class GlobalSettings_SaveData : SaveDataNonAlloc
    {
        public BoolSettingList_SaveData BoolSettings = new();
        public FloatSettingList_SaveData FloatSettings = new();
        public EnumSettingList_SaveData EnumSettings = new();

        public override void ResetCachedData()
        {
            BoolSettings.ResetCachedData();
            FloatSettings.ResetCachedData();
            EnumSettings.ResetCachedData();
        }
    }

    public sealed class BoolSettingList_SaveData : ListSaveDataNonAlloc<BoolSetting_SaveData>
    {
        public void Add(SettingName name, bool value)
        {
            var data = AddValue();
            data.SetData(name, value);
        }
    }

    public sealed class BoolSetting_SaveData : SaveDataNonAlloc
    {
        public SettingName SettingName;
        public bool Value;

        public void SetData(SettingName name, bool value)
        {
            SettingName = name;
            Value = value;
        }

        public override void ResetCachedData()
        {
        }
    }

    public sealed class FloatSettingList_SaveData : ListSaveDataNonAlloc<FloatSetting_SaveData>
    {
        public void Add(SettingName name, float value)
        {
            var data = AddValue();
            data.SetData(name, value);
        }
    }

    public sealed class FloatSetting_SaveData : SaveDataNonAlloc
    {
        public SettingName SettingName;
        public float Value;

        public void SetData(SettingName name, float value)
        {
            SettingName = name;
            Value = value;
        }

        public override void ResetCachedData()
        {
        }
    }

    public sealed class EnumSettingList_SaveData : ListSaveDataNonAlloc<EnumSetting_SaveData>
    {
        public void Add(SettingName name, int value)
        {
            var data = AddValue();
            data.SetData(name, value);
        }
    }

    public sealed class EnumSetting_SaveData : SaveDataNonAlloc
    {
        public SettingName SettingName;
        public int Value;

        public void SetData(SettingName name, int value)
        {
            SettingName = name;
            Value = value;
        }

        public override void ResetCachedData()
        {
        }
    }
}