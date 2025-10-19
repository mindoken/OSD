using Infrastructure;

namespace MetaGame
{
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