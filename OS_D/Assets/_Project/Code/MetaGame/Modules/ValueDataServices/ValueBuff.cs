namespace MetaGame
{
    public sealed class ValueBuff<T>
    {
        public T Value { get; private set; }
        public int Order { get; private set; }
        public ValueOperation Operation { get; private set; }

        public ValueBuff(T value, int order, ValueOperation operation)
        {
            Value = value;
            Order = order;
            Operation = operation;
        }

        public void SetValue(T value)
        {
            Value = value;
        }
    }
}