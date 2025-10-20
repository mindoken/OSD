using App;
using Infrastructure;
using UniRx;
using Utils;

namespace UI
{
    public sealed class BoolSettingPresenter : IBoolSettingPresenter
    {
        private const string SETTING_NAME_PREFIX = "SETTINGS_NAME_";
        private const string OFF_KEY = "SETTINGS_BoolOff";
        private const string ON_KEY = "SETTINGS_BoolOn";

        public IReadOnlyReactiveProperty<string> SettingName => _settingName;
        private readonly ReactiveProperty<string> _settingName = new();
        public IReadOnlyReactiveProperty<string> MarkerStatusText => _markerStatusText;
        private readonly ReactiveProperty<string> _markerStatusText = new();
        public IReadOnlyReactiveProperty<bool> MarkerStatus => _markerStatus;
        private readonly ReactiveProperty<bool> _markerStatus = new();
        public IReadOnlyReactiveProperty<bool> IsFasterTag => _isFasterTag;
        private readonly ReactiveProperty<bool> _isFasterTag = new();

        private readonly ILocalization _locale;
        private readonly BoolSetting _model;
        private readonly CompositeDisposable _compositeDisposable = new();

        public BoolSettingPresenter(
            ILocalization locale,
            BoolSetting model)
        {
            _locale = locale;
            _model = model;
            _model.Value.Subscribe(value => OnValueChanged(value)).AddTo(_compositeDisposable);
            _locale.OnLocaleChanged += OnLocaleChanged;

            _settingName.Value = _locale.LocalizeString(SETTING_NAME_PREFIX + EnumUtils<SettingName>.ToString(_model.Name));
        }

        private void OnLocaleChanged()
        {
            _settingName.Value = _locale.LocalizeString(SETTING_NAME_PREFIX + EnumUtils<SettingName>.ToString(_model.Name));

            var key = _markerStatus.Value ? ON_KEY : OFF_KEY;
            _markerStatusText.Value = _locale.LocalizeString(key);
        }

        private void OnValueChanged(bool value)
        {
            _markerStatus.Value = value;

            var key = value ? ON_KEY : OFF_KEY;
            _markerStatusText.Value = _locale.LocalizeString(key);

            if (_model.FasterTag != FasterTag.None && 
                (value == true && _model.FasterTag == FasterTag.True ||
                value == false && _model.FasterTag == FasterTag.False))
                _isFasterTag.Value = true;
            else
                _isFasterTag.Value = false;

        }

        public void OnMarkerClicked()
        {
            var value = _model.Value.Value;
            _model.Value.Value = !value;
        }

        public void Dispose()
        {
            _locale.OnLocaleChanged -= OnLocaleChanged;
            _compositeDisposable.Dispose();
        }
    }
}