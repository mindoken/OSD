using System;
using UnityEngine;

namespace MetaGame
{
    [Serializable]
    public sealed class UpgradeMetadata
    {
        public string TitleLocaleKey;
        public string DescriptionLocaleKey;
        public Sprite Icon;
    }
}