using Cysharp.Threading.Tasks;

namespace Infrastructure
{
    public interface ISaveLoadSystem
    {
        UniTask Save();
        void Load();
    }
}