using Infrastructure;
using CoreGame.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using CoreGame;

internal sealed class AllView_UninstallSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<AllView_UninstallRequest>> _filter = EcsWorlds.UNITS;
    private readonly EcsPoolInject<AllView_UninstallRequest> _requestPool = EcsWorlds.UNITS;
    private readonly EcsPoolInject<UnityViewModel> _viewModelPool = EcsWorlds.UNITS;

    void IEcsRunSystem.Run(IEcsSystems systems)
    {
        foreach (var entity in _filter.Value)
        {
            ref var viewModel = ref _viewModelPool.Value.Get(entity);
            if (viewModel.CurrentView != null)
            {
                ref var requestData = ref _requestPool.Value.Get(entity);
                //if culled
                viewModel.MemoryPool.UnspawnItem(viewModel.CurrentView);
                viewModel.CurrentView = null;
            }
            _requestPool.Value.Del(entity);
        }
    }
}