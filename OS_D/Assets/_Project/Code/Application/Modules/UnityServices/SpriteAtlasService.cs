using Alchemy.Inspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace App
{
    public sealed class SpriteAtlasService
    {
        private readonly Dictionary<string, SpriteAtlas> _catalog = new();

        public SpriteAtlasService(SpriteAtlasInfo[] atlas)
        {
            for (int i = 0; i < atlas.Length; i++)
            {
                _catalog.Add(atlas[i].key, atlas[i].atlas);
            }
        }

        public Sprite GetSprite(string atlas, string name)
        {
            return _catalog[atlas].GetSprite(name);
        }

        public SpriteAtlas GetAtlas(string atlas)
        {
            return _catalog[atlas];
        }
    }

    [Serializable]
    [Group]
    public sealed class SpriteAtlasInfo
    {
        [HorizontalGroup("1")][HideLabel] public string key;
        [HorizontalGroup("1")][HideLabel] public SpriteAtlas atlas;
    }
}