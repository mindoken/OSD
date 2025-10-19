using Cysharp.Threading.Tasks;

namespace Application
{
    public interface ILoadTask
    {
        string Title { get; }
        UniTask LoadAsync();
    }
}