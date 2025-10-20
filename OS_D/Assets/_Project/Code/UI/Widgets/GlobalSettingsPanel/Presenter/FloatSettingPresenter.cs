using App;
using Infrastructure;
using UniRx;
using Utils;

namespace UI
{
    public sealed class FloatSettingPresenter : IFloatSettingPresenter
    {
        private const string SETTING_NAME_PREFIX = "SETTINGS_NAME_";

        public IReadOnlyReactiveProperty<string> SettingName => _settingName;
        private readonly ReactiveProperty<string> _settingName = new();
        public IReadOnlyReactiveProperty<float> FloatValue => _floatValue;
        private readonly ReactiveProperty<float> _floatValue = new();
        public float MinValue => _model.MinValue;
        public float MaxValue => _model.MaxValue;
        public bool WholeNumbers => _model.WholeNumbers;

        private readonly FloatSetting _model;
        private readonly ILocalization _locale;
        private readonly CompositeDisposable _compositeDisposable;

        public FloatSettingPresenter(
            ILocalization locale,
            FloatSetting model)
        {
            _model = model;
            _locale = locale;

            _compositeDisposable = new();
            _settingName.Value = _locale.LocalizeString(SETTING_NAME_PREFIX + EnumUtils<SettingName>.ToString(_model.Name));
            _floatValue.Value = _model.Value.Value;
            _model.Value.Subscribe(value => OnValueChangedFromModel(value)).AddTo(_compositeDisposable);
            _locale.OnLocaleChanged += OnLocaleChanged;
        }

        private void OnValueChangedFromModel(float value)
        {
            _floatValue.Value = value;
        }

        public void OnValueChangedFromSlider(float value)
        {
            _model.Value.Value = value;
        }

        private void OnLocaleChanged()
        {
            _settingName.Value = _locale.LocalizeString(SETTING_NAME_PREFIX + EnumUtils<SettingName>.ToString(_model.Name));
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
            _locale.OnLocaleChanged -= OnLocaleChanged;
        }
    }
}