using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace MetaGame
{
    [CreateAssetMenu(fileName = "UpgradesManager_Installer", menuName = "MetaGame/UpgradesManager/New Installer")]
    public sealed class UpgradesManager_Pipeline : ScriptableObject
    {
        [Title("Upgrades")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All, ShowFoldoutHeader = false, Reorderable = true)]
        public UpgradeConfig[] Upgrades;
    }
}