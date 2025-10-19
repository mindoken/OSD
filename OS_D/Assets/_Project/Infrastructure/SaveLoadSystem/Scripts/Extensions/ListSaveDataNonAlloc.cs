using System.Collections.Generic;

namespace Infrastructure
{
    public abstract class ListSaveDataNonAlloc<T> : SaveDataNonAlloc
        where T : SaveDataNonAlloc, new()
    {
        public List<T> CachedList = new();
        public int Size;

        protected T AddValue()
        {
            T value;
            if (CachedList.Count > Size)
                value = CachedList[Size];
            else
                value = new T(); CachedList.Add(value);
            Size++;
            return value;
        }

        public override void ResetCachedData()
        {
            for (int i = 0; i < CachedList.Count; i++)
                CachedList[i].ResetCachedData();
            Size = 0;
        }

        public void ClearAlloc()
        {
            CachedList.Clear();
        }
    }
}