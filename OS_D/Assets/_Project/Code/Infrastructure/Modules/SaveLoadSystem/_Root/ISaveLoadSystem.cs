using Cysharp.Threading.Tasks;

namespace App
{
    public interface ISaveLoadSystem
    {
        UniTask Save();
        void Load();
    }
}