using Leopotam.EcsLite;

namespace Infrastructure
{
    public interface IEntityInstaller
    {
        void Install(EcsWorld world, int entity, bool culled = false);
        void Dispose(EcsWorld world, int entity);
    }
}