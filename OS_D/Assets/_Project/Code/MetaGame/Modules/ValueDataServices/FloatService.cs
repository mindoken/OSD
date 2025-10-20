using System;
using System.Collections.Generic;

namespace MetaGame
{
    public sealed class FloatService
    {
        public event Action OnValueRecalculated;

        private readonly double _baseValue;
        public double Value { get; private set; }

        public IReadOnlyDictionary<string, ValueBuff<double>> Buffs => _buffs;
        private readonly Dictionary<string, ValueBuff<double>> _buffs = new();

        private readonly List<ValueBuff<double>> _cahce = new();

        public FloatService(double baseValue)
        {
            _baseValue = baseValue;
            Value = _baseValue;
        }

        private void RecalculateValue()
        {
            _cahce.Clear();
            foreach (var buff in _buffs.Values)
            {
                _cahce.Add(buff);
            }
            _cahce.Sort((a, b) => a.Order.CompareTo(b.Order));

            Value = _baseValue;
            for (int i = 0; i < _cahce.Count; i++)
            {
                var buff = _cahce[i];
                
                if (buff.Operation == ValueOperation.ADD)
                    Value += buff.Value;
                else if (buff.Operation == ValueOperation.MUL)
                    Value *= buff.Value;
            }
            OnValueRecalculated?.Invoke();
        }

        public void SetBuff(string key, ValueBuff<double> buff)
        {
            if (_buffs.ContainsKey(key))
                _buffs[key] = buff;
            else
                _buffs.Add(key, buff);
            RecalculateValue();
        }

        public void SetBuffNonAlloc(string key, double value, int order, ValueOperation operation)
        {
            if (_buffs.ContainsKey(key))
                _buffs[key].SetValue(value);
            else
                _buffs.Add(key, new ValueBuff<double>(value, order, operation));
            RecalculateValue();
        }

        public void RemoveBuff(string key)
        {
            if (_buffs.ContainsKey(key))
                _buffs.Remove(key);

            RecalculateValue();
        }
    }
}