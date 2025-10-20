using System;
using UnityEngine;

namespace MetaGame
{
    [CreateAssetMenu(fileName = "CurrencyCellConfig", menuName = "MetaGame/CurrencyBank/New CurrencyCellConfig")]
    public sealed class CurrencyCellConfig : ScriptableObject
    {
        private const float SPACE_HEIGHT = 10.0f;

        public CurrencyName key;

        [Space(SPACE_HEIGHT)]
        public CurrencyCellMetaData metaData;

        [Space(SPACE_HEIGHT)]
        public CurrencyCellInitialData initialData;

        [Space(SPACE_HEIGHT)]
        public CurrencyCellHarvestViewData harvestData;
    }
}