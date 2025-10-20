using System.Collections.Generic;

namespace MetaGame
{
    public interface ICurrencyBank
    {
        bool TryGetCell(CurrencyName key, out CurrencyCell cell);
        CurrencyCell GetCell(CurrencyName key);
        IEnumerator<CurrencyCell> GetEnumerator();
        bool Spend(CurrencyData currency);
        bool Spend(IEnumerable<CurrencyData> range);
        bool IsEnough(CurrencyData currency);
        bool IsEnough(IEnumerable<CurrencyData> range);
    }
}