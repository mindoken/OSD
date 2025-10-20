using UnityEngine;
using Zenject;

namespace MetaGame
{
    [CreateAssetMenu(fileName = "BeehiveCountUpgrade", menuName = "MetaGame/UpgradesManager/New BeehiveCountUpgrade")]
    public sealed class BeehiveCount_UpgradeConfig : UpgradeConfig
    {
        public string beehiveId;

        public override Upgrade InstantiateUpgrade()
        {
            return new BeehiveCountUpgrade(this);
        }

        protected override void Validate()
        {
            base.Validate();
        }
    }
}