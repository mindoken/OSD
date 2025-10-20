using Cysharp.Threading.Tasks;

namespace App
{
    public interface ILoadTask
    {
        string Title { get; }
        UniTask LoadAsync();
    }
}