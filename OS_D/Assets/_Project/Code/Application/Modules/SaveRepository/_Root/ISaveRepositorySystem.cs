using Cysharp.Threading.Tasks;

namespace App
{
    public interface ISaveRepositorySystem
    {
        string CurrentSaveProfile { get; }
        ISaveRepository ApplicationRepository { get; }
        ISaveRepository GameplayRepository { get; }
        UniTask LoadRepositories();
        UniTask SaveRepositories();
    }
}