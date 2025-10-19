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
}