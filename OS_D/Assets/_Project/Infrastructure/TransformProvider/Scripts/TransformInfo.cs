using Alchemy.Inspector;
using System;
using UnityEngine;

namespace Infrastructure
{
    [Serializable]
    [Group]
    public struct TransformInfo
    {
        [HorizontalGroup("1")][HideLabel] public TransformName Name;
        [HorizontalGroup("1")][HideLabel] public Transform Transform;
    }
}