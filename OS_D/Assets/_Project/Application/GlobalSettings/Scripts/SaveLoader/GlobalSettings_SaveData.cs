using Infrastructure;

namespace Application
{
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