using System.Collections.Generic;

namespace MetaGame
{
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