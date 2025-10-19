using UniRx;

namespace UI
{
    public interface IEnumSettingPresenter : ISettingPresenter
    {
        IReadOnlyReactiveProperty<string> SettingName { get; }
        IReadOnlyReactiveProperty<string> CurrentEnumName { get; }
        IReadOnlyReactiveProperty<bool> IsFasterTag { get; }
        void OnRightButtonClicked();
        void OnLeftButtonClicked();
    }
}