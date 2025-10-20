using Alchemy.Inspector;
using App;
using Infrastructure;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [CreateAssetMenu(fileName = "GlobalSettingsPanel_Pipeline", menuName = "UI/GlobalSettingsPanel/New Pipeline")]
    public sealed class GlobalSettingsPanel_Pipeline : ScriptableObject
    {
        public WidgetName Name;
        public GlobalSettingsPanel Prefab;
        public bool ShowOnStart;

        [Title("Settings Groups To Show")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = false,
            Reorderable = true)]
        public SettingGroupConfig[] Groups;
    }

    [Serializable]
    [Group]
    public struct SettingGroupConfig
    {
        public string GroupTitleKey;

        [Title("Settings")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = false,
            Reorderable = true)]
        public SettingInfo[] Settings;
    }

    [Serializable]
    [Group]
    public struct SettingInfo
    {
        [HorizontalGroup("Group1")] public SettingName Setting;
        [HorizontalGroup("Group1")] public SettingType Type;
    }

    public enum SettingType
    {
        Bool = 0,
        Float = 1,
        Enum = 2
    }
}