using UniRx;

namespace UI
{
    public interface IBoolSettingPresenter : ISettingPresenter
    {
        IReadOnlyReactiveProperty<string> SettingName { get; }
        IReadOnlyReactiveProperty<string> MarkerStatusText { get; }
        IReadOnlyReactiveProperty<bool> MarkerStatus { get; }
        IReadOnlyReactiveProperty<bool> IsFasterTag { get; }
        void OnMarkerClicked();
    }
}