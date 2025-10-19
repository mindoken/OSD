using System;
using UnityEngine;

namespace MetaGame
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        protected const float SPACE_HEIGHT = 10.0f;

        public string id;
        
        [Range(2, 99)] public int maxLevel = 2;

        [Space(SPACE_HEIGHT)]
        public UpgradeMetadata metadata;

        [Space(SPACE_HEIGHT)]
        public UpgradeInitialStats initialStats;

        [Space(SPACE_HEIGHT)]
        [SerializeField] public CurrencyName priceType;
        [SerializeReference] public IValueTable priceTable;

        public abstract Upgrade InstantiateUpgrade();

        private void OnValidate()
        {
            try
            {
                this.Validate();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        protected virtual void Validate()
        {
            this.priceTable.OnValidate(this.maxLevel);
        }
    }
}