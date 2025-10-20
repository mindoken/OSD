using Infrastructure;
using MetaGame;
using UnityEngine;

namespace UI
{
    public sealed class CurrencyHintPopupPresenter : IHintPopupPresenter
    {
        public string TitleText => _title;
        private readonly string _title;
        public string DescriptionText => _description;
        private readonly string _description;
        public Sprite Icon => _icon;
        private readonly Sprite _icon;

        public CurrencyHintPopupPresenter(
            CurrencyName currencyName,
            CurrencyBank bank,
            ILocalization locale)
        {
            var currency = bank.GetCell(currencyName).Config;
            _icon = currency.metaData.Icon;
            _title = locale.LocalizeString(currency.metaData.NameKey);
            _description = locale.LocalizeString(currency.metaData.HintDescriptionKey);
        }
    }

    public sealed class CurrencyHintPopupPresenterFactory
    {
        private readonly CurrencyBank _bank;
        private readonly ILocalization _locale;

        public CurrencyHintPopupPresenterFactory(
            CurrencyBank bank,
            ILocalization locale)
        {
            _bank = bank;
            _locale = locale;
        }

        public CurrencyHintPopupPresenter Create(CurrencyName currencyName)
        {
            return new CurrencyHintPopupPresenter(currencyName, _bank, _locale);
        }
    }
}