using Application.SaveRepository;

namespace Infrastructure
{
    public interface ISaveLoader
    {
        void SaveData(ISaveRepository repository);
        void LoadData(ISaveRepository repository);
    }
}