using UnityEngine;

namespace CoreGame
{
    public interface IUnityBodyView
    {
        Transform RootTransform { get; }
        SpriteRenderer Renderer { get; }
    }
}