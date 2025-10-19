using System.Collections.Generic;

namespace MetaGame
{
    public interface IValueDataServices
    {
        IReadOnlyDictionary<FloatServiceName, FloatService> FloatServices { get; }
        IReadOnlyDictionary<BoolServiceName, BoolService> BoolServices { get; }
        FloatService GetFloatService(FloatServiceName service);
        BoolService GetBoolService(BoolServiceName service);
    }
}