using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace App
{
    [CreateAssetMenu(fileName ="GlobalSettingsPipeline", menuName ="Application/GlobalSettings/New Pipeline")]
    public sealed class GlobalSettingsPipeline : ScriptableObject
    {
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = true,
            Reorderable = true)]
        [LabelText("Bool Settings")]
        public BoolSetting[] BoolSettings;

        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = true,
            Reorderable = true)]
        [LabelText("Float Settings")]
        public FloatSetting[] FloatSettings;

        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = true,
            Reorderable = true)]
        [LabelText("Enum Settings")]
        public EnumSetting[] EnumSettings;

        [Button]
        void Validate()
        {
            for (int i = 0; i < EnumSettings.Length; i++)
            {
                EnumSettings[i].OnValidate();
            }
        }
    }
}