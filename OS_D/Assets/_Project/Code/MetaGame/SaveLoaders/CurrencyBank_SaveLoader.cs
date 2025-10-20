using App;
using Infrastructure;
using UnityEngine;

namespace MetaGame
{
    public sealed class CurrencyBank_SaveLoader : SaveLoader<CurrencyBank, CurrencyBank_SaveData>
    {
        private readonly CurrencyBank_SaveData _saveData = new();

        protected override CurrencyBank_SaveData ConvertToData(CurrencyBank service)
        {
            _saveData.ResetCachedData();

            foreach (CurrencyCell cell in service)
            {
                _saveData.Add(cell.Config.key, cell.Amount.Value);
            }
            return _saveData;
        }

        protected override void SetupData(CurrencyBank service, CurrencyBank_SaveData data)
        {
            for (int i = 0; i < data.Size; i++)
            {
                var cell = data.CachedList[i];
                if (service.TryGetCell(cell.Key, out CurrencyCell bankCell))
                {
                    bankCell.Change(cell.Amount);
                }
            }
        }
    }

    public sealed class CurrencyBank_SaveData : ListSaveDataNonAlloc<CurrencySaveData>
    {
        public void Add(CurrencyName key, long amount)
        {
            var value = AddValue();
            value.SetData(key, amount);
        }
    }

    public sealed class CurrencySaveData : SaveDataNonAlloc
    {
        public CurrencyName Key;
        public long Amount;

        public void SetData(CurrencyName key, long amount)
        {
            Key = key;
            Amount = amount;
        }

        public override void ResetCachedData()
        {
        }
    }
}