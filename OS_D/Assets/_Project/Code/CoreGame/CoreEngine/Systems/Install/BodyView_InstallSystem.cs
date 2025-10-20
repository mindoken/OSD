using Infrastructure;
using CoreGame.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using CoreGame;

internal sealed class BodyView_InstallSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<AllView_InstallRequest>> _allInstallFilter = EcsWorlds.UNITS;

    private readonly EcsPoolInject<UnityViewModel> _viewModelPool = EcsWorlds.UNITS;
    private readonly EcsPoolInject<PositionModel> _modelPool = EcsWorlds.UNITS;
    private readonly EcsPoolInject<UnityBodyView> _viewPool = EcsWorlds.UNITS;

    private readonly EcsPoolInject<UnityTransformWithPosition_SynchronizationRequest> _unityTransformSyncRequest = EcsWorlds.UNITS;

    void IEcsRunSystem.Run(IEcsSystems systems)
    {
        foreach (var entity in _allInstallFilter.Value)
        {
            if (!_modelPool.Value.Has(entity))
                continue;
            if (_viewPool.Value.Has(entity))
                continue;

            if (_viewModelPool.Value.Get(entity).CurrentView is IUnityBodyView bodyView)
            {
                _viewPool.Value.Add(entity).Value = bodyView;

                _unityTransformSyncRequest.Value.Add(entity);
            }
        }
    }
}
