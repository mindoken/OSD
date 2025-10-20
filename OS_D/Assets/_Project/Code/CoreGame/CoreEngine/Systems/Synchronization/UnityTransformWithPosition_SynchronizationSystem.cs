using Infrastructure;
using CoreGame.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

internal sealed class UnityTransformWithPosition_SynchronizationSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<UnityTransformWithPosition_SynchronizationRequest>> _filter = EcsWorlds.UNITS;
    private readonly EcsPoolInject<UnityTransformWithPosition_SynchronizationRequest> _requestPool = EcsWorlds.UNITS;

    private readonly EcsPoolInject<UnityBodyView> _viewPool = EcsWorlds.UNITS;
    private readonly EcsPoolInject<PositionModel> _positionPool = EcsWorlds.UNITS;

    void IEcsRunSystem.Run(IEcsSystems systems)
    {

        foreach (int entity in _filter.Value)
        {
            var position = _positionPool.Value.Get(entity).Position;
            var view = _viewPool.Value.Get(entity).Value;
            view.RootTransform.position = position;
            view.Renderer.sortingOrder = -(int)position.y;

            _requestPool.Value.Del(entity);
        }
    }
}