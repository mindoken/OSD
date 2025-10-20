using Cysharp.Threading.Tasks;
using System;

namespace App
{
    public interface ISaveRepository
    {
        SaveRepositoryName Name { get; }
        event Action OnCurrentKeyChanged;
        string CurrentKey { get; }
        void SetCurrentSaveKey(string key);
        bool TryGetData<T>(out T data);
        void SetData<T>(T data);
        bool IsRepositoryEmpty(string key);
        UniTask Load(string[] keys);
        UniTask SaveCurrent();
        UniTask Delete(string key);
    }
}