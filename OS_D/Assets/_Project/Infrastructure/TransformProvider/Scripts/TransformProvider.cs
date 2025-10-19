using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public sealed class TransformProvider : ITransformProvider
    {
        private readonly Dictionary<TransformName, Transform> _transforms = new();

        public TransformProvider(
            TransformInfo[] transforms)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                var info = transforms[i];
                _transforms.Add(info.Name, info.Transform);
            }
        }

        public Transform GetTransform(TransformName name)
        {
            return _transforms[name];
        }

        public bool TryGetTransform(TransformName name, out Transform transform)
        {
            return _transforms.TryGetValue(name, out transform);
        }
    }
}