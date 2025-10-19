using Cysharp.Threading.Tasks;

namespace Application
{
    public interface IAssetLoader
    {
        UniTask LoadAsset();
    }
}