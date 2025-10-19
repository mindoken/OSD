using UnityEngine;

namespace App
{
    public interface IPrefabFactory
    {
        T CreatePrefab<T>(Object prefab, Transform parent) where T : MonoBehaviour;
        T CreatePrefab<T>(T prefab, Transform parent) where T : Component;
    }
}