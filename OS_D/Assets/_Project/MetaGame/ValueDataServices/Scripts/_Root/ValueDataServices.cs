using System.Collections.Generic;

namespace MetaGame
{
    public sealed class ValueDataServices : IValueDataServices
    {
        public IReadOnlyDictionary<FloatServiceName, FloatService> FloatServices => _floatServices;
        private readonly Dictionary<FloatServiceName, FloatService> _floatServices = new();

        public IReadOnlyDictionary<BoolServiceName, BoolService> BoolServices => _boolServices;
        private readonly Dictionary<BoolServiceName, BoolService> _boolServices = new();

        public ValueDataServices(ValueDataServices_Pipeline pipeline)
        {
            var floatServices = pipeline.FloatServices;
            var boolServices = pipeline.BoolServices;

            for (int i = 0; i < floatServices.Length; i++)
            {
                var service = floatServices[i];
                _floatServices.Add(service.Key, new FloatService(service.BaseValue));
            }

            for (int i = 0; i < boolServices.Length; i++)
            {
                var service = boolServices[i];
                _boolServices.Add(service.Key, new BoolService(service.BaseValue));
            }
        }

        public FloatService GetFloatService(FloatServiceName service) => _floatServices[service];
        public BoolService GetBoolService(BoolServiceName service) => _boolServices[service];
    }
}