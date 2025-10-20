using Alchemy.Inspector;
using System;
using UnityEngine;
using Zenject;

namespace MetaGame
{
    public sealed class UpgradesManagerDebug : MonoBehaviour
    {
        public UpgradesManager _manager;

        [Inject]
        public void Construct(UpgradesManager manager)
        {
            _manager = manager;
        }

        [Button]
        public void LevelUp(string upgradeId)
        {
            _manager.LevelUp(upgradeId);
        }
    }
}