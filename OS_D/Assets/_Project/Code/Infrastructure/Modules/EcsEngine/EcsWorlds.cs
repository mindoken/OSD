
namespace Infrastructure
{
    public static class EcsWorlds
    {
        public static readonly string[] WORLDS = new string[3]
        {
            "UNITS",
            "EVENTS",
            "REQUESTS"
        };

        public static readonly string UNITS = WORLDS[0];
        public static readonly string EVENTS = WORLDS[1];
        public static readonly string REQUESTS = WORLDS[2];
    }
}