using UniRx;

namespace UI
{
    public interface IFloatSettingPresenter : ISettingPresenter
    {
        IReadOnlyReactiveProperty<string> SettingName { get; }
        IReadOnlyReactiveProperty<float> FloatValue { get; }
        float MinValue { get; }
        float MaxValue { get; }
        bool WholeNumbers { get; }
        void OnValueChangedFromSlider(float value);
    }
}