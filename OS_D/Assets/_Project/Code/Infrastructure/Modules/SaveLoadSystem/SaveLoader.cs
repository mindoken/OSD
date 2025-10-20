using Zenject;

namespace App
{
    public abstract class SaveLoader<TService, TData> : ISaveLoader
    {
        [Inject]
        private readonly TService _service;

        public void LoadData(ISaveRepository repository)
        {
            if (repository.TryGetData(out TData data))
            {
                this.SetupData(_service, data);
            }
            else
            {
                this.SetupDefaultData(_service);
            }
        }

        public void SaveData(ISaveRepository repository)
        {
            TData data = this.ConvertToData(_service);
            repository.SetData(data);
        }

        protected abstract TData ConvertToData(TService service);
        protected abstract void SetupData(TService service, TData data);
        protected virtual void SetupDefaultData(TService service) { }
    }
}