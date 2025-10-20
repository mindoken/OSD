using Common;
using Infrastructure;
using MetaGame;
using UniRx;
using UnityEngine;

namespace UI
{
    public sealed class CurrencyPresenter : ICurrencyPresenter
    {
        public ReactiveCommand SpendCurrencyAnimation { get; private set; } = new();
        public ReactiveCommand EarnCurrencyAnimation { get; private set; } = new();
        public IReadOnlyReactiveProperty<string> Currency => _currency;
        private readonly ReactiveProperty<string> _currency = new();
        public Sprite Icon => _model.Config.metaData.Icon;

        private readonly CurrencyCell _model;
        private readonly ILocalization _locale;
        private readonly IPopupManager _popupManager;
        private readonly CurrencyHintPopupPresenterFactory _popupPresenterFactory;

        private readonly CompositeDisposable _compositeDisposable = new();

        public CurrencyPresenter(
            CurrencyCell model,
            ILocalization locale,
            IPopupManager popupManager,
            CurrencyHintPopupPresenterFactory popupPresenterFactory)
        {
            _popupManager = popupManager;
            _popupPresenterFactory = popupPresenterFactory;
            _model = model;
            _locale = locale;

            _locale.OnLocaleChanged += OnLocaleChanged;
            _model.Amount.Pairwise().Subscribe(value => OnAmountChanged(value.Current, value.Previous)).AddTo(_compositeDisposable);
            _currency.Value = _locale.FormatAndLocalizeCurrency(_model.Amount.Value); 
        }

        private void OnAmountChanged(long current, long previous)
        {
            if (current >= previous)
            {
                EarnCurrencyAnimation.Execute();
                _currency.Value = _locale.FormatAndLocalizeCurrency(current);
            }
            else
            {
                SpendCurrencyAnimation.Execute();
                _currency.Value = _locale.FormatAndLocalizeCurrency(current);
            }
        }

        private void OnLocaleChanged()
        {
            _currency.Value = _locale.FormatAndLocalizeCurrency(_model.Amount.Value);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
            _locale.OnLocaleChanged -= OnLocaleChanged;
        }

        public void ShowHintPopup()
        {
            _popupManager.ShowPopup(
                PopupName.Hint,
                _popupPresenterFactory.Create(_model.Config.key));
        }

        public void HideHintPopup()
        {
            _popupManager.HideCurrentPopup();
        }
    }
}