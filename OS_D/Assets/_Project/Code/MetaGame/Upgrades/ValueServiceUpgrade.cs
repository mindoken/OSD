using Zenject;

namespace MetaGame
{
    public sealed class ValueServiceUpgrade : Upgrade
    {
        private readonly ValueService_UpgradeConfig _config;

        private readonly FloatServiceName _service;
        private readonly string _buffId;
        private readonly ValueOperation _operation;
        private readonly int _operationOrder;

        private ValueDataServices _services;

        public ValueServiceUpgrade(
            ValueService_UpgradeConfig config,
            FloatServiceName service,
            string buffId,
            ValueOperation operation,
            int order) : base(config)
        {
            _config = config;
            _service = service;
            _buffId = buffId;
            _operation = operation;
            _operationOrder = order;
        }

        [Inject]
        public void Construct(ValueDataServices services)
        {
            _services = services;
        }

        protected override void LevelUp(int level)
        {
            var service = _services.GetFloatService(_service); //todo

            var value = _config.ValueTable.GetValue(level);
            service.SetBuffNonAlloc(_buffId, value, _operationOrder, _operation);
        }
    }
}