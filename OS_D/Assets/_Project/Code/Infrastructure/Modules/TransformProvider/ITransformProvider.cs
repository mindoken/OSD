using UnityEngine;

namespace Infrastructure
{
    public interface ITransformProvider
    {
        Transform GetTransform(TransformName name);
        bool TryGetTransform(TransformName name, out Transform transform);
    }
}