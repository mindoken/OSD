using Zenject;
using UnityEngine;

namespace App
{
    public sealed class PrefabFactory : IPrefabFactory
    {
        private readonly IInstantiator _instantiator;

        public PrefabFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public T CreatePrefab<T>(Object prefab, Transform parent) where T : MonoBehaviour
        {
            T prefabHandler = _instantiator.InstantiatePrefabForComponent<T>(prefab, parent);
            return prefabHandler;
        }

        public T CreatePrefab<T>(T prefab, Transform parent) where T : Component
        {
            T prefabHandler = _instantiator.InstantiatePrefabForComponent<T>(prefab, parent);
            return prefabHandler;
        }
    }
}