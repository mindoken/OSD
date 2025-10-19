using Common;
using Infrastructure;
using MetaGame;
using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public sealed class UpgradePreviewPresenter : IUpgradePreviewPresenter
    {
        public Sprite UpgradeIcon => _model.Metadata.Icon;
        public IReadOnlyReactiveProperty<string> UpgradeTitle => _upgradeTitle;
        private readonly ReactiveProperty<string> _upgradeTitle = new();
        public IReadOnlyReactiveProperty<string> UpgradeLevel => _upgradeLevel;
        private readonly ReactiveProperty<string> _upgradeLevel = new();
        public IReadOnlyReactiveProperty<Sprite> CurrencyIcon => _currencyIcon;
        private readonly ReactiveProperty<Sprite> _currencyIcon = new();
        public IReadOnlyReactiveProperty<string> CurrencyAmount => _currencyAmount;
        private readonly ReactiveProperty<string> _currencyAmount = new();
        public IReadOnlyReactiveProperty<bool> IsHidden => _isHidden;
        private readonly ReactiveProperty<bool> _isHidden = new();
        public IReadOnlyReactiveProperty<bool> IsMaxLevel => _isMaxLevel;
        private readonly ReactiveProperty<bool> _isMaxLevel = new();

        private readonly Upgrade _model;
        private readonly CurrencyBank _bank;
        private readonly IUpgradeViewPresenter _mainUpgradeView;
        private readonly ILocalization _locale;

        public UpgradePreviewPresenter(
            Upgrade upgrade,
            CurrencyBank bank,
            IUpgradeViewPresenter mainUpgradeView,
            ILocalization locale)
        {
            _model = upgrade;
            _bank = bank;
            _locale = locale;
            _model.OnLevelUp += OnLevelUp;
            _model.OnOpened += OnUpgradeOpened;
            _locale.OnLocaleChanged += OnLocaleChanged;
            _mainUpgradeView = mainUpgradeView;

            _isMaxLevel.Value = _model.IsMaxLevel;
            UpdateUpgradeInfo();
        }

        private void UpdateUpgradeInfo()
        {
            if (_model.IsOpen)
            {
                _isHidden.Value = false;
                UpdateNextPrice();
                _upgradeTitle.Value = _locale.LocalizeString(_model.Metadata.TitleLocaleKey);
                _upgradeLevel.Value = $"{_model.Level} / {_model.MaxLevel}";
            }
            else
            {
                _isHidden.Value = true;
                _currencyIcon.Value = null;
                _currencyAmount.Value = "???";
                _upgradeLevel.Value = "?? / ??";
                _upgradeTitle.Value = "???";
            }
        }

        private void UpdateNextPrice()
        {
            if (_model.IsMaxLevel)
            {
                _currencyAmount.Value = null;
                _currencyIcon.Value = null;
            }
            else
            {
                _currencyAmount.Value = _locale.FormatAndLocalizeCurrency(_model.NextPrice.amount);
                _currencyIcon.Value = _bank.GetCell(_model.NextPrice.key).Config.metaData.Icon;
            }
        }

        private void OnLocaleChanged()
        {
            if (_isHidden.Value)
                return;
            _upgradeTitle.Value = _locale.LocalizeString(_model.Metadata.TitleLocaleKey);

            if (_model.IsMaxLevel)
                return;
            _currencyAmount.Value = _locale.FormatAndLocalizeCurrency(_model.NextPrice.amount);
        }

        private void OnUpgradeOpened() => UpdateUpgradeInfo();

        private void OnLevelUp(int _)
        {
            _upgradeLevel.Value = $"{_model.Level} / {_model.MaxLevel}";
            UpdateNextPrice();
            if (_model.IsMaxLevel)
                _isMaxLevel.Value = true;
        }

        public void OnUpgradeClicked()
        {
            _mainUpgradeView.SetUpgrade(_model);
        }

        public void Dispose()
        {
            _model.OnLevelUp -= OnLevelUp;
            _model.OnOpened -= OnUpgradeOpened;
            _locale.OnLocaleChanged -= OnLocaleChanged;
        }
    }
}