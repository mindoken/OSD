using Infrastructure;

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
}