using Common;
using Infrastructure;
using MetaGame;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public sealed class CurrencyEnoughButtonPresenter : ICurrencyEnoughButtonPresenter
    {
        public event Action OnButtonClicked;

        public IReadOnlyReactiveProperty<Sprite> CurrencyIcon => _currencyIcon;
        private readonly ReactiveProperty<Sprite> _currencyIcon = new();
        public IReadOnlyReactiveProperty<string> CurrencyText => _currencyText;
        private readonly ReactiveProperty<string> _currencyText = new();
        public IReadOnlyReactiveProperty<bool> IsCurrencyEnough => _isCurrencyEnough;
        private readonly ReactiveProperty<bool> _isCurrencyEnough = new();
        public IReadOnlyReactiveProperty<bool> IsButtonBlocked => _isButtonBlocked;
        private readonly ReactiveProperty<bool> _isButtonBlocked = new();
        public IReadOnlyReactiveProperty<string> BlockingButtonText => _blockingButtonText;
        private ReactiveProperty<string> _blockingButtonText = new();
        public IReadOnlyReactiveProperty<string> MainButtonText => _mainButtonText;
        private ReactiveProperty<string> _mainButtonText = new();

        private CurrencyData _model;
        private CurrencyCell _cell;
        private string _blockingButtonTextKey;
        private string _mainButtonTextKey;
        private readonly ILocalization _locale;
        private readonly CurrencyBank _bank;

        private readonly CompositeDisposable _compositeDisposable = new();

        public CurrencyEnoughButtonPresenter(
            CurrencyData model,
            CurrencyBank bank,
            ILocalization locale,
            string mainButtonTextKey = null)
        {
            _bank = bank;
            _locale = locale;
            _locale.OnLocaleChanged += OnLocaleChanged;
            SetNewCurrencyData(model);
            _mainButtonTextKey = mainButtonTextKey;
            RefreshMainButtonText();
        }

        public void SetNewCurrencyData(CurrencyData model)
        {
            DisposeCurrencyData();
            _model = model;

            _cell = _bank.GetCell(model.key);

            _cell.Amount.Subscribe(_ => CheckIsCurrencyEnough()).AddTo(_compositeDisposable);
            _isButtonBlocked.Value = false;
            CheckIsCurrencyEnough();
            _currencyIcon.Value = _cell.Config.metaData.Icon;
            _currencyText.Value = _locale.FormatAndLocalizeCurrency(_model.amount);  
        }

        public void SetNewButtonBlockedText(string key)
        {
            DisposeCurrencyData();
            _blockingButtonTextKey = key;
            _blockingButtonText.Value = _locale.LocalizeString(key);
            _isButtonBlocked.Value = true;
            _cell = null;
        }

        private void CheckIsCurrencyEnough()
        {
            _isCurrencyEnough.Value = _bank.IsEnough(_model);
        }

        private void RefreshMainButtonText()
        {
            _mainButtonText.Value = _mainButtonTextKey == null ? null : _locale.LocalizeString(_mainButtonTextKey);
        }

        private void OnLocaleChanged()
        {
            if (_cell == null)
                _blockingButtonText.Value = _locale.LocalizeString(_blockingButtonTextKey);
            RefreshMainButtonText();
            _currencyText.Value = _locale.FormatAndLocalizeCurrency(_model.amount);
        }

        public void ClickButton()
        {
            OnButtonClicked?.Invoke();
        }

        private void DisposeCurrencyData()
        {
            if (_cell == null)
                return;
            _compositeDisposable.Clear();
        }

        public void Dispose()
        {
            DisposeCurrencyData();
            _locale.OnLocaleChanged -= OnLocaleChanged;
        }
    }
}