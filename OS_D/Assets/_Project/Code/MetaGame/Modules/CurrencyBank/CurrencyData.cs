using System;

namespace MetaGame
{
    [Serializable]
    public struct CurrencyData
    {
        public CurrencyName key;
        public long amount;

        public static CurrencyData Add(CurrencyData data, long value)
        {
            data.amount += value;
            return data;
        }

        public static CurrencyData Multiply(CurrencyData data, long value)
        {
            data.amount *= value;
            return data;
        }

        public static CurrencyData operator +(CurrencyData left, CurrencyData right)
        {
            if (left.key == right.key)
                return Add(left, right.amount);
            else
                throw new ArgumentException("The currency data keys must be similar!");
        }

        public static CurrencyData operator *(CurrencyData left, long right)
        {
            return Multiply(left, right);
        }

        public static CurrencyData Add(CurrencyData data, int value)
        {
            data.amount += value;
            return data;
        }

        public static CurrencyData Multiply(CurrencyData data, int value)
        {
            data.amount *= value;
            return data;
        }

        public static CurrencyData operator *(CurrencyData left, int right)
        {
            return Multiply(left, right);
        }

        public static CurrencyData operator +(CurrencyData left, int right)
        {
            return Add(left, right);
        }
    }
}