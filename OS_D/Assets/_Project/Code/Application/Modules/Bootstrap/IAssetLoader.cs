using Cysharp.Threading.Tasks;

namespace App
{
    public interface IAssetLoader
    {
        UniTask LoadAsset();
    }
}