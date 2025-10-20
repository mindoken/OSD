using App;
using Infrastructure;

namespace MetaGame
{
    public sealed class ValueDataServices_SaveLoader : SaveLoader<ValueDataServices, ValueDataServices_SaveData>
    {
        private readonly ValueDataServices_SaveData _saveData = new();

        protected override ValueDataServices_SaveData ConvertToData(ValueDataServices service)
        {
            _saveData.ResetCachedData();

            foreach (var pair in service.FloatServices)
            {
                var floatService = _saveData.FloatServices.Add((int)pair.Key);
                foreach (var buff in pair.Value.Buffs)
                {
                    var buffInfo = buff.Value;
                    floatService.Add(buff.Key, buffInfo.Value, buffInfo.Order, (int)buffInfo.Operation);
                }
            }

            foreach (var pair in service.BoolServices)
            {
                var boolService = _saveData.BoolServices.Add((int)pair.Key);
                foreach (var buff in pair.Value.Buffs)
                {
                    var buffInfo = buff.Value;
                    boolService.Add(buff.Key, buffInfo.Value, buffInfo.Order, (int)buffInfo.Operation);
                }
            }

            return _saveData;
        }

        protected override void SetupData(ValueDataServices service, ValueDataServices_SaveData data)
        {
            var floatServices = data.FloatServices.CachedList;
            for (int i = 0; i < data.FloatServices.Size; i++)
            {
                var floatService = floatServices[i];
                var target = service.GetFloatService((FloatServiceName)floatService.ServiceName);
                for (int k = 0; k < floatService.Size; k++)
                {
                    var buff = floatService.CachedList[k];
                    target.SetBuffNonAlloc(buff.Id, buff.Value, buff.Order, (ValueOperation)buff.Operation);
                }
            }

            var boolServices = data.BoolServices.CachedList;
            for (int i = 0; i < data.BoolServices.Size; i++)
            {
                var boolService = boolServices[i];
                var target = service.GetBoolService((BoolServiceName)boolService.ServiceName);
                for (int k = 0; k < boolService.Size; k++)
                {
                    var buff = boolService.CachedList[k];
                    target.SetBuffNonAlloc(buff.Id, buff.Value, buff.Order, (ValueOperation)buff.Operation);
                }
            }
        }
    }

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