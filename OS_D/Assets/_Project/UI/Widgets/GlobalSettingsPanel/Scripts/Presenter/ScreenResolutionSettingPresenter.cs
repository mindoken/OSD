using Application;
using Infrastructure;
using UniRx;
using Utils;

namespace UI
{
    public sealed class ScreenResolutionSettingPresenter : IEnumSettingPresenter
    {
        private const string SETTING_NAME_PREFIX = "SETTINGS_NAME_";

        public IReadOnlyReactiveProperty<string> SettingName => _settingName;
        private readonly ReactiveProperty<string> _settingName = new();
        public IReadOnlyReactiveProperty<string> CurrentEnumName => _currentEnumName;
        private readonly ReactiveProperty<string> _currentEnumName = new();
        public IReadOnlyReactiveProperty<bool> IsFasterTag => _isFasterTag;
        private readonly ReactiveProperty<bool> _isFasterTag = new();

        private readonly CompositeDisposable _compositeDisposable;
        private readonly EnumSetting _model;
        private readonly ILocalization _locale;
        private readonly ScreenResolutionService _resolutionService;

        public ScreenResolutionSettingPresenter(
            ScreenResolutionService resolutionService,
            ILocalization locale,
            EnumSetting model)
        {
            _compositeDisposable = new();
            _model = model;
            _locale = locale;
            _resolutionService = resolutionService;
            _locale.OnLocaleChanged += OnLocaleChanged;

            _model.Value.Subscribe(value => OnValueChanged(value)).AddTo(_compositeDisposable);
            _settingName.Value = _locale.LocalizeString(SETTING_NAME_PREFIX + EnumUtils<SettingName>.ToString(_model.Name));
        }

        private void OnValueChanged(int value)
        {
            _currentEnumName.Value = _resolutionService.Resolutions[value].ToString();

            if (_model.FasterTag != FasterTag.None && (
                (_model.FasterTag == FasterTag.Min && value == 0) ||
                (_model.FasterTag == FasterTag.Max && value == _resolutionService.Resolutions.Count - 1)))
            {
                _isFasterTag.Value = true;
            }
            else
            {
                _isFasterTag.Value = false;
            }
        }

        private void OnLocaleChanged()
        {
            _settingName.Value = _locale.LocalizeString(SETTING_NAME_PREFIX + EnumUtils<SettingName>.ToString(_model.Name));
        }

        public void OnLeftButtonClicked()
        {
            var value = _model.Value.Value;
            if (value == 0)
            {
                value = _resolutionService.Resolutions.Count - 1;
            }
            else
            {
                value--;
            }
            _model.Value.Value = value;
        }

        public void OnRightButtonClicked()
        {
            var value = _model.Value.Value;
            if (value == _resolutionService.Resolutions.Count - 1)
            {
                value = 0;
            }
            else
            {
                value++;
            }
            _model.Value.Value = value;
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
            _locale.OnLocaleChanged -= OnLocaleChanged;
        }
    }
}