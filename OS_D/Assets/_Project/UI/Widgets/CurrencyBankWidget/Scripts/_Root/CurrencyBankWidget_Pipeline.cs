using Alchemy.Inspector;
using Infrastructure;
using MetaGame;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [CreateAssetMenu(fileName = "CurrencyBankWidget_Pipeline", menuName = "UI/CurrencyBankWidget/New Pipeline")]
    public sealed class CurrencyBankWidget_Pipeline : ScriptableObject
    {
        public WidgetName Name;
        public CurrencyBankWidget Prefab;
        public bool ShowOnStart;

        [Title("Currency To Show")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = false,
            Reorderable = true)] [Group]
        public CurrencyName[] Cells;
    }
}