using Common;
using Infrastructure;
using MetaGame;
using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public sealed class UpgradeViewPresenter : IUpgradeViewPresenter
    {
        private readonly string MAX_KEY = "_Max";
        private readonly string HIDDEN_KEY = "_???";
        private readonly string UPGRADE_KEY = "_Upgrade";
        private readonly string HIDDEN_DESCRIPTION_KEY = "UPGRADE_DESCRIPTION_???";

        public ICurrencyEnoughButtonPresenter CurrencyEnoughButtonPresenter => _currencyEnoughButtonPresenter;
        private ICurrencyEnoughButtonPresenter _currencyEnoughButtonPresenter;

        public IReadOnlyReactiveProperty<string> UpgradeTitle => _upgradeTitle;
        private readonly ReactiveProperty<string> _upgradeTitle = new();
        public IReadOnlyReactiveProperty<Sprite> UpgradeIcon => _upgradeIcon;
        private readonly ReactiveProperty<Sprite> _upgradeIcon = new();
        public IReadOnlyReactiveProperty<string> UpgradeLevel => _upgradeLevel;
        private readonly ReactiveProperty<string> _upgradeLevel = new();
        public IReadOnlyReactiveProperty<string> UpgradeDescription => _upgradeDescription;
        private readonly ReactiveProperty<string> _upgradeDescription = new();
        public IReadOnlyReactiveProperty<bool> IsHidden => _isHidden;
        private readonly ReactiveProperty<bool> _isHidden = new();

        private Upgrade _model;

        private readonly UpgradesManager _upgradesManager;
        private readonly ILocalization _localization;

        public UpgradeViewPresenter(
            Upgrade upgrade,
            CurrencyBank bank,
            UpgradesManager upgradesManager,
            ILocalization localization)
        {
            _upgradesManager = upgradesManager;
            _localization = localization;

            _localization.OnLocaleChanged += OnLocaleChanged;

            _currencyEnoughButtonPresenter = new CurrencyEnoughButtonPresenter(new CurrencyData(), bank, localization, UPGRADE_KEY);
            _currencyEnoughButtonPresenter.OnButtonClicked += OnUpgradeClicked;
            SetUpgrade(upgrade);
        }

        public void SetUpgrade(Upgrade upgrade)
        {
            DisposeUpgrade();
            _model = upgrade;

            _model.OnLevelUp += OnUpgradeLevelUp;
            _model.OnOpened += OnUpgradeOpened;

            _upgradeIcon.Value = _model.Metadata.Icon;
            if (_model.IsOpen)
            {
                _upgradeTitle.Value = _localization.LocalizeString(_model.Metadata.TitleLocaleKey);
                _upgradeLevel.Value = $"{_model.Level} / {_model.MaxLevel}";
                _upgradeDescription.Value = _localization.LocalizeString(_model.Metadata.DescriptionLocaleKey);
                _isHidden.Value = false;
            }
            else
            {
                _upgradeDescription.Value = _localization.LocalizeString(HIDDEN_DESCRIPTION_KEY);
                _upgradeLevel.Value = "?? / ??";
                _upgradeTitle.Value = "???";
                _isHidden.Value = true;
            }

            UpdateUpgradeButton();
        }

        private void UpdateUpgradeButton()
        {
            if (!_model.IsOpen)
                _currencyEnoughButtonPresenter.SetNewButtonBlockedText(HIDDEN_KEY);
            else if (!_model.IsMaxLevel)
                _currencyEnoughButtonPresenter.SetNewCurrencyData(_model.NextPrice);
            else
                _currencyEnoughButtonPresenter.SetNewButtonBlockedText(MAX_KEY);
        }

        private void OnUpgradeOpened()
        {
            _upgradeTitle.Value = _localization.LocalizeString(_model.Metadata.TitleLocaleKey);
            _upgradeLevel.Value = $"{_model.Level} / {_model.MaxLevel}";
            _upgradeDescription.Value = _localization.LocalizeString(_model.Metadata.DescriptionLocaleKey);
            _isHidden.Value = false;
            UpdateUpgradeButton();
        }

        private void OnUpgradeLevelUp(int _)
        {
            _upgradeLevel.Value = $"{_model.Level} / {_model.MaxLevel}";
            UpdateUpgradeButton();
        }

        private void OnLocaleChanged()
        {
            if (_model == null)
                return;
            if (_model.IsOpen)
            {
                _upgradeTitle.Value = _localization.LocalizeString(_model.Metadata.TitleLocaleKey);
                _upgradeDescription.Value = _localization.LocalizeString(_model.Metadata.DescriptionLocaleKey);
            }
            else
            {
                _upgradeDescription.Value = _localization.LocalizeString(HIDDEN_DESCRIPTION_KEY);
            }
        }

        public void OnUpgradeClicked()
        {
            if (_model == null)
                return;
            _upgradesManager.LevelUp(_model);
        }

        private void DisposeUpgrade()
        {
            if (_model == null)
                return;
            _model.OnLevelUp -= OnUpgradeLevelUp;
            _model.OnOpened -= OnUpgradeOpened;
        }

        public void Dispose()
        {
            DisposeUpgrade();
            _localization.OnLocaleChanged -= OnLocaleChanged;
            _currencyEnoughButtonPresenter.OnButtonClicked -= OnUpgradeClicked;
        }

    }
}