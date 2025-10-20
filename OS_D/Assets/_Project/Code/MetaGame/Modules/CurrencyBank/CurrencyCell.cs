using UniRx;

namespace MetaGame
{
    public sealed class CurrencyCell
    {
        public CurrencyCellConfig Config => _config;
        private readonly CurrencyCellConfig _config;

        public IReadOnlyReactiveProperty<long> Amount => _amount;
        private readonly ReactiveProperty<long> _amount = new();

        public CurrencyCell(CurrencyCellConfig config)
        {
            _config = config;
            _amount.Value = config.initialData.amount;
        }

        public bool Add(long range)
        {
            if (range <= 0)
                return false;

            _amount.Value += range;
            return true;
        }

        public bool Spend(long range)
        {
            if (range <= 0)
                return false;

            if (_amount.Value < range)
                return false;

            _amount.Value -= range;
            return true;
        }

        public void Change(long amount)
        {
            if (_amount.Value != amount)
                _amount.Value = amount;
        }

        public bool IsEnough(long range)
        {
            return _amount.Value >= range;
        }
    }    
}