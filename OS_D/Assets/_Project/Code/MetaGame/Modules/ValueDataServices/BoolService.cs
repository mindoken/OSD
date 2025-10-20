using System;
using System.Collections.Generic;

namespace MetaGame
{
    public sealed class BoolService
    {
        public event Action OnValueRecalculated;

        private readonly bool _baseValue;
        public bool Value { get; private set; }

        public IReadOnlyDictionary<string, ValueBuff<bool>> Buffs => _buffs;
        private readonly Dictionary<string, ValueBuff<bool>> _buffs = new();

        private readonly List<ValueBuff<bool>> _cahce = new();

        public BoolService(bool baseValue)
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
                    Value = Value || buff.Value;
                else if (buff.Operation == ValueOperation.MUL)
                    Value = Value && buff.Value;
            }
            OnValueRecalculated?.Invoke();
        }

        public void SetBuff(string key, ValueBuff<bool> buff)
        {
            if (_buffs.ContainsKey(key))
                _buffs[key] = buff;
            else
                _buffs.Add(key, buff);
            RecalculateValue();
        }

        public void SetBuffNonAlloc(string key, bool value, int order, ValueOperation operation)
        {
            if (_buffs.ContainsKey(key))
                _buffs[key].SetValue(value);
            else
                _buffs.Add(key, new ValueBuff<bool>(value, order, operation));
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