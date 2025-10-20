using Alchemy.Inspector;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MetaGame
{
    [CreateAssetMenu(fileName = "PlayerDataServices_Pipeline", menuName = "MetaGame/PlayerDataServices/New Pipeline")]
    public sealed class ValueDataServices_Pipeline : ScriptableObject
    {
        [Title("Float Services")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = false,
            Reorderable = true)]
        [LabelText("Value Services")]
        public FloatServiceInitialData[] FloatServices;

        [Title("Bool Services")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = false,
            Reorderable = true)]
        [LabelText("Value Services")]
        public BoolServiceInitialData[] BoolServices;
    }

    [Serializable]
    [Group]
    public sealed class FloatServiceInitialData
    {
        [HorizontalGroup("1")][HideLabel] public FloatServiceName Key;
        [HorizontalGroup("1")][HideLabel] public double BaseValue;
    }

    [Serializable]
    [Group]
    public sealed class BoolServiceInitialData
    {
        [HorizontalGroup("1")][HideLabel] public BoolServiceName Key;
        [HorizontalGroup("1")][HideLabel] public bool BaseValue;
    }
}