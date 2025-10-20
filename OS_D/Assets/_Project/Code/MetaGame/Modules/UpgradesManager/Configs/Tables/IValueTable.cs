
namespace MetaGame
{
    public interface IValueTable
    {
        double GetValue(int level);
        void OnValidate(int maxLevel);
    }
}