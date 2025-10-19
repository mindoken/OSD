using Infrastructure;

namespace MetaGame
{
    public sealed class ValueDataServices_SaveData : SaveDataNonAlloc
    {
        public ValueDataServices_SaveData<double> FloatServices = new();
        public ValueDataServices_SaveData<bool> BoolServices = new();

        public override void ResetCachedData()
        {
            FloatServices.ResetCachedData();
            BoolServices.ResetCachedData();
        }
    }

    public sealed class ValueDataServices_SaveData<T> : ListSaveDataNonAlloc<ValueService_SaveData<T>>
    {
        public ValueService_SaveData<T> Add(int name)
        {
            var item = AddValue();
            item.SetData(name);
            return item;
        }
    }

    public sealed class ValueService_SaveData<T> : ListSaveDataNonAlloc<ValueBuff_SaveData<T>>
    {
        public int ServiceName;

        public void SetData(int serviceName)
        {
            ServiceName = serviceName;
        }

        public void Add(string id, T value, int order, int operation)
        {
            var item = AddValue();
            item.SetData(id, value, order, operation);
        }
    }

    public sealed class ValueBuff_SaveData<T> : SaveDataNonAlloc
    {
        public string Id;
        public T Value;
        public int Order;
        public int Operation;

        public void SetData(string id, T value, int order, int operation)
        {
            Id = id;
            Value = value;
            Order = order;
            Operation = operation;
        }

        public override void ResetCachedData()
        {
        }
    }
}