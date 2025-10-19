using Infrastructure;

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
}