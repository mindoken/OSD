using System.Collections.Generic;

namespace MetaGame
{
    public interface IUpgradesManager
    {
        IReadOnlyDictionary<string, Upgrade> Upgrades { get; }
        Upgrade GetUpgrade(string id);
        bool CanLevelUp(Upgrade upgrade);
        void LevelUp(Upgrade upgrade);
        bool CanLevelUp(string id);
        void LevelUp(string id);
    }
}