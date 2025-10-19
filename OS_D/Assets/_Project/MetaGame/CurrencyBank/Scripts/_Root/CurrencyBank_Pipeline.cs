using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace MetaGame
{
    [CreateAssetMenu(fileName = "CurrencyBank_Pipeline", menuName = "MetaGame/CurrencyBank/New Pipeline")]
    public sealed class CurrencyBank_Pipeline : ScriptableObject
    {
        [Title("Currency Cells")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All, ShowFoldoutHeader = false, Reorderable = true)]
        public CurrencyCellConfig[] CurrencyCells;
    }
}