using System;

namespace Infrastructure
{
    public interface ILocalization
    {
        event Action OnLocaleChanged;
        int CurrentLocalIndex { get; }
        string GetLocalNameByIndex(int index);
        string[] GetAllLocalNamesByIndex();
        void SetLocalByIndex(int index);
        string LocalizeString(string key);
        string LocalizeNumber<T>(T number, int decimalPlaces = 0) 
            where T: struct, IConvertible, IComparable, IFormattable;
        string FormatAndLocalizeCurrency(long currency);
    }
}