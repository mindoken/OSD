namespace Common
{
    public static class ExecutionOrders
    {
        public static readonly int PRE_PRE_INIT_SYSTEM = -2;
        public static readonly int PRE_INIT_SYSTEM = -1;
        public static readonly int DEFAULT = 0;
        public static readonly int SAVELOADER = 10;
        public static readonly int AFTER_LOADING = 11;
        public static readonly int WORLD_VIEW = 15;
        public static readonly int INTERFACE = 20;
    }
}