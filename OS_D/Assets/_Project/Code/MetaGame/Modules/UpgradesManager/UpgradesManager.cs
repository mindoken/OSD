using Alchemy.Inspector;
using System;
using System.Collections.Generic;
using Zenject;

namespace MetaGame
{
    [Serializable]
    public sealed class UpgradesManager : IUpgradesManager
    {
        public IReadOnlyDictionary<string, Upgrade> Upgrades => _upgrades;
        [ReadOnly, ShowInInspector] private readonly Dictionary<string, Upgrade> _upgrades = new();

        private readonly CurrencyBank _bank;

        public UpgradesManager(
            CurrencyBank bank,
            UpgradesManager_Pipeline pipeline,
            DiContainer Container)
        {
            _bank = bank;
            var upgrades = pipeline.Upgrades;
            for (int i = 0; i < upgrades.Length; i++)
            {
                var upgrade = upgrades[i].InstantiateUpgrade();
                Container.Inject(upgrade);
                _upgrades[upgrade.Id] = upgrade;
            }
        }

        public Upgrade GetUpgrade(string id)
        {
            return _upgrades[id];
        }

        public bool CanLevelUp(Upgrade upgrade)
        {
            if (upgrade.IsMaxLevel)
            {
                return false;
            }

            var price = upgrade.NextPrice;
            if (!_bank.IsEnough(new List<CurrencyData> { price }))
            {
                return false;
            }

            return upgrade.CanLevelUp();
        }

        public void LevelUp(Upgrade upgrade)
        {
            if (!CanLevelUp(upgrade))
            {
                return;
                //throw new Exception($"Can not level up {upgrade.Id}");
            }

            var price = upgrade.NextPrice;
            _bank.Spend(new List<CurrencyData> { price });

            upgrade.LevelUp();
        }

        public bool CanLevelUp(string id)
        {
            var upgrade = _upgrades[id];
            return CanLevelUp(upgrade);
        }

        public void LevelUp(string id)
        {
            var upgrade = _upgrades[id];
            LevelUp(upgrade);
        }
    }
}