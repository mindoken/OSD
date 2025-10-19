using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Application.SaveRepository
{
    public sealed class SaveRepository : ISaveRepository
    {
        public SaveRepositoryName Name => _name;
        private readonly SaveRepositoryName _name;

        private Dictionary<string, Dictionary<string, string>> _repositories = new();

        public event Action OnCurrentKeyChanged;

        public string CurrentKey => _currentKey;
        private string _currentKey = string.Empty;

        private readonly ISaveStrategy _saveStrategy;
        private readonly List<UniTask> _taskCahce = new();

        public SaveRepository(
            SaveRepositoryName name,
            ISaveStrategy saveStrategy)
        {
            _name = name;
            _saveStrategy = saveStrategy;
        }

        public void SetCurrentSaveKey(string key)
        {
            _currentKey = key;
            if (!_repositories.ContainsKey(key))
            {
                _repositories[key] = new();
            }
            OnCurrentKeyChanged?.Invoke();
            Debug.Log($"<color=green>Current key: {key} In {_name}!</color>");
        }

        public async UniTask Load(string[] keys)
        {
            _taskCahce.Clear();
            for (int i = 0; i < keys.Length; i++)
            {
                _taskCahce.Add(Load(keys[i]));
            }
            await UniTask.WhenAll(_taskCahce);
        }

        private async UniTask Load(string key)
        {
            _repositories[key] = await _saveStrategy.LoadRepository(key);
        }

        public async UniTask SaveCurrent()
        {
            await Save(_currentKey);
        }

        private async UniTask Save(string key)
        {
            if (key == String.Empty)
                return;

            await _saveStrategy.SaveRepository(_repositories[key], key);
            Debug.Log($"<color=green>Repository saved for Key: {key} In {_name}!</color>");
        }

        public void SetData<T>(T data)
        {
            string key = typeof(T).Name;

            var jsonData = JsonConvert.SerializeObject(data);
            var repository = _repositories[_currentKey];
            repository[key] = jsonData;
        }

        public bool TryGetData<T>(out T data)
        {
            string key = typeof(T).Name;

            if (_repositories.TryGetValue(_currentKey, out var repository))
            {
                if (repository.TryGetValue(key, out var jsonData))
                {
                    try
                    {
                        data = JsonConvert.DeserializeObject<T>(jsonData);
                        return true;
                    }
                    catch
                    {
                        data = default;
                        return false;
                    }
                }
            }
            data = default;
            return false;
        }

        public async UniTask Delete(string key)
        {
            if (_repositories.ContainsKey(key))
            {
                _repositories.Remove(key);
                await _saveStrategy.DeleteRepository(key);
            }
        }

        public bool IsRepositoryEmpty(string key)
        {
            if (_repositories.TryGetValue(key, out var repository))
            {
                return repository.Count == 0;
            }
            else
            {
                return true;
            }
        }
    }
}