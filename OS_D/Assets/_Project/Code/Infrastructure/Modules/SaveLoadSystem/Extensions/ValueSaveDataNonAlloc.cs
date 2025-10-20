namespace Infrastructure
{
    public abstract class ValueSaveDataNonAlloc<T1> : SaveDataNonAlloc
    {
        public T1 Value1;

        public virtual void SetValue(T1 value1)
        {
            Value1 = value1;
        }

        public override void ResetCachedData()
        {
            Value1 = default;
        }
    }

    public abstract class ValueSaveDataNonAlloc<T1, T2> : SaveDataNonAlloc
    {
        public T1 Value1;
        public T2 Value2;

        public virtual void SetValue(T1 value1, T2 value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public override void ResetCachedData()
        {
            Value1 = default;
            Value2 = default;
        }
    }

    public abstract class ValueSaveDataNonAlloc<T1, T2, T3> : SaveDataNonAlloc
    {
        public T1 Value1;
        public T2 Value2;
        public T3 Value3;

        public virtual void SetValue(T1 value1, T2 value2, T3 value3)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }

        public override void ResetCachedData()
        {
            Value1 = default;
            Value2 = default;
            Value3 = default;
        }
    }
}