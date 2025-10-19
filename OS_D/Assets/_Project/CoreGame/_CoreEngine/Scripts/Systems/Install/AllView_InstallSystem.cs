using Infrastructure;
using CoreGame.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using CoreGame;

internal sealed class AllView_InstallSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<AllView_InstallRequest>> _filter = EcsWorlds.UNITS;
    private readonly EcsPoolInject<AllView_InstallRequest> _requestPool = EcsWorlds.UNITS;
    private readonly EcsPoolInject<UnityViewModel> _viewModelPool = EcsWorlds.UNITS;

    void IEcsRunSystem.Run(IEcsSystems systems)
    {
        foreach (var entity in _filter.Value)
        {
            ref var viewModel = ref _viewModelPool.Value.Get(entity);
            if (viewModel.CurrentView != null)
                continue;

            ref var requestData = ref _requestPool.Value.Get(entity);
            viewModel.CurrentView = viewModel.MemoryPool.SpawnItem();
            viewModel.CurrentView.Initialize(requestData.Culled);
        }
    }
}
