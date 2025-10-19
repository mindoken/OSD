using Infrastructure;
using CoreGame.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using CoreGame;

internal sealed class BodyView_UninstallSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<AllView_UninstallRequest>> _allUninstallFilter = EcsWorlds.UNITS;
    private readonly EcsPoolInject<UnityBodyView> _viewPool = EcsWorlds.UNITS;

    void IEcsRunSystem.Run(IEcsSystems systems)
    {
        foreach (var entity in _allUninstallFilter.Value)
        {
            if (!_viewPool.Value.Has(entity))
                continue;
            _viewPool.Value.Del(entity);
        }
    }
}
