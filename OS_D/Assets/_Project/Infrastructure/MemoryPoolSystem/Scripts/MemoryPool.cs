using App;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    public sealed class MemoryPool<T> : IMemoryPool
        where T : Component
    {
        MemoryPoolName IMemoryPool.Name => _name;

        private readonly T _itemPrefab;
        private readonly Transform _container;
        private readonly int _initValue;
        private readonly MemoryPoolName _name;

        public IReadOnlyList<T> ActiveItems => _activeItems;
        private readonly List<T> _activeItems = new();
        private readonly Queue<T> _freeList = new();

        private readonly IPrefabFactory _prefabFactory;

        public MemoryPool(
            IPrefabFactory prefabFactory,
            ITransformProvider transformProvider,
            MemoryPoolInfo<T> info)
        {
            _prefabFactory = prefabFactory;
            _itemPrefab = info.Prefab;
            _container = transformProvider.GetTransform(info.TransformName);
            _initValue = info.InitialCount;
            _name = info.Name;
        }

        public MemoryPool(
            T itemPrefab,
            Transform container)
        {
            _itemPrefab = itemPrefab;
            _container = container;
        }

        void IMemoryPool.Load()
        {
            //load prefab
        }

        void IMemoryPool.Init()
        {
            for (int i = 0; i < _initValue; i++)
                SpawnItem();

            Clear();
        }

        public T SpawnItem()
        {
            if (_freeList.TryDequeue(out var item))
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                if (_prefabFactory == null)
                    item = Object.Instantiate(_itemPrefab, _container);
                else
                    item = _prefabFactory.CreatePrefab(_itemPrefab, _container);
            }

            _activeItems.Add(item);
            return item;
        }

        public void UnspawnItem(T item)
        {
            if (item != null && _activeItems.Remove(item))
            {
                item.gameObject.SetActive(false);
                _freeList.Enqueue(item);
            }
        }

        public void Clear()
        {
            for (int i = 0, count = _activeItems.Count; i < count; i++)
            {
                T item = _activeItems[i];
                item.gameObject.SetActive(false);
                _freeList.Enqueue(item);
            }

            _activeItems.Clear();
        }
    }
}