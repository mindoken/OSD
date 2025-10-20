using App;
using Infrastructure;
using UniRx;
using Utils;

namespace UI
{
    public sealed class EnumSettingPresenter : IEnumSettingPresenter
    {
        private const string SETTING_NAME_PREFIX = "SETTINGS_NAME_";
        private const string SETTING_ENUM_PREFIX = "SETTINGS_ENUM_";

        public IReadOnlyReactiveProperty<string> SettingName => _settingName;
        private readonly ReactiveProperty<string> _settingName = new();
        public IReadOnlyReactiveProperty<string> CurrentEnumName => _currentEnumName;
        private readonly ReactiveProperty<string> _currentEnumName = new();
        public IReadOnlyReactiveProperty<bool> IsFasterTag => _isFasterTag;
        private readonly ReactiveProperty<bool> _isFasterTag = new();

        private readonly CompositeDisposable _compositeDisposable;
        private readonly EnumSetting _model;
        private readonly ILocalization _locale;

        public EnumSettingPresenter(
            ILocalization locale,
            EnumSetting model)
        {
            _compositeDisposable = new();
            _model = model;
            _locale = locale;
            _locale.OnLocaleChanged += OnLocaleChanged;

            _model.Value.Subscribe(value => OnValueChanged(value)).AddTo(_compositeDisposable);
            _settingName.Value = _locale.LocalizeString(SETTING_NAME_PREFIX + EnumUtils<SettingName>.ToString(_model.Name));
        }

        private void OnValueChanged(int value)
        {
            _currentEnumName.Value = _locale.LocalizeString(SETTING_ENUM_PREFIX + _model.GetEntryString(value));

            var entryIndex = _model.GetEntryIndex(value);
            if (_model.FasterTag != FasterTag.None && (
                (_model.FasterTag == FasterTag.Min && entryIndex == 0) ||
                (_model.FasterTag == FasterTag.Max && entryIndex == _model.Entries.Count - 1)))
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
            _currentEnumName.Value = _locale.LocalizeString(SETTING_ENUM_PREFIX + _model.GetEntryString(_model.Value.Value));
        }

        public void OnLeftButtonClicked() => _model.MoveLeft();

        public void OnRightButtonClicked() => _model.MoveRight();

        public void Dispose()
        {
            _compositeDisposable.Dispose();
            _locale.OnLocaleChanged -= OnLocaleChanged;
        }
    }
}