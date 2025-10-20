using System;
using System.Collections.Generic;

namespace MetaGame
{
    public sealed class CurrencyBank : ICurrencyBank
    {
        private readonly Dictionary<CurrencyName, CurrencyCell> _cells = new();

        public CurrencyBank(CurrencyBank_Pipeline pipeline)
        {
            var cells = pipeline.CurrencyCells;
            for (var i = 0; i < cells.Length; i++)
            {
                var cell = cells[i];
                _cells.Add(cell.key, new CurrencyCell(cell));
            }
        }

        public bool TryGetCell(CurrencyName key, out CurrencyCell cell)
        {
            return _cells.TryGetValue(key, out cell);
        }

        public CurrencyCell GetCell(CurrencyName key)
        {
            return _cells[key];
        }

        public IEnumerator<CurrencyCell> GetEnumerator()
        {
            return _cells.Values.GetEnumerator();
        }

        public bool Spend(CurrencyData currency)
        {
            if (!this.IsEnough(currency))
            {
                return false;
            }
            CurrencyCell cell = _cells[currency.key];
            cell.Spend(currency.amount);
            return true;
        }

        public bool Spend(IEnumerable<CurrencyData> range)
        {
            if (!this.IsEnough(range))
            {
                return false;
            }

            foreach (CurrencyData currency in range)
            {
                CurrencyCell cell = _cells[currency.key];
                cell.Spend(currency.amount);
            }

            return true;
        }

        public bool IsEnough(CurrencyData currency)
        {
            if (!_cells.TryGetValue(currency.key, out CurrencyCell cell))
            {
                throw new ArgumentException($"Currency type {currency.key} is not found in the Bank!");
            }

            if (!cell.IsEnough(currency.amount))
            {
                return false;
            }

            return true;
        }

        public bool IsEnough(IEnumerable<CurrencyData> range)
        {
            foreach (CurrencyData currency in range)
            {
                if (!_cells.TryGetValue(currency.key, out CurrencyCell cell))
                {
                    throw new ArgumentException($"Currency type {currency.key} is not found in the Bank!");
                }

                if (!cell.IsEnough(currency.amount))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

