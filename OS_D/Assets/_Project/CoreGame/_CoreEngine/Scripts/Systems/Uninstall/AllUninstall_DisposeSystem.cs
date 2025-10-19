using Infrastructure;
using CoreGame.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

internal sealed class AllUninstall_DisposeSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<AllEntity_UninstallRequest>> _filter = EcsWorlds.UNITS;
    private readonly EcsWorldInject _units = EcsWorlds.UNITS;

    private readonly EcsFilterInject<Inc<AllView_InstallRequest>> _allInstallFilter = EcsWorlds.UNITS;
    private readonly EcsPoolInject<AllView_InstallRequest> _allInstallRequestPool = EcsWorlds.UNITS;

    void IEcsRunSystem.Run(IEcsSystems systems)
    {
        foreach (var entity in _allInstallFilter.Value)
        {
            _allInstallRequestPool.Value.Del(entity);
        }

        foreach (var entity in _filter.Value)
        {
            _units.Value.DelEntity(entity);
        }
    }
}

