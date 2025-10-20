using App;
using System.Collections.Generic;

namespace MetaGame
{
    public sealed class UpgradesManager_SaveLoader : SaveLoader<UpgradesManager, UpgradesManager_SaveData>
    {
        protected override UpgradesManager_SaveData ConvertToData(UpgradesManager service)
        {
            var saveData = new UpgradesManager_SaveData();
            saveData.Upgrades = new();

            foreach(var pair in service.Upgrades)
            {
                var upgrade = new Upgrade_SaveData { Id = pair.Key, CurrentLevel = pair.Value.Level, IsOpen = pair.Value.IsOpen };
                saveData.Upgrades.Add(upgrade.Id, upgrade);
            }

            return saveData;
        }

        protected override void SetupData(UpgradesManager service, UpgradesManager_SaveData data)
        {
            foreach (var pair in data.Upgrades)
            {
                var upgrade = service.GetUpgrade(pair.Key);
                upgrade.SetupLevel(pair.Value.CurrentLevel);
                upgrade.SetupOpen(pair.Value.IsOpen);
            }
        }
    }

    public sealed class UpgradesManager_SaveData
    {
        public Dictionary<string, Upgrade_SaveData> Upgrades;
    }

    public sealed class Upgrade_SaveData
    {
        public string Id;
        public int CurrentLevel;
        public bool IsOpen;
    }
}