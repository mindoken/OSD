using Cysharp.Threading.Tasks;

namespace App
{
    public interface ISaveLoader
    {
        void SaveData(ISaveRepository repository);
        void LoadData(ISaveRepository repository);
    }
}