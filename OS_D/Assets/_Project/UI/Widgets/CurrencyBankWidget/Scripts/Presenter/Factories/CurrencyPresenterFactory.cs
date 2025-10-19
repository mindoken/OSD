using Infrastructure;
using MetaGame;

namespace UI
{
    public sealed class CurrencyPresenterFactory
    {
        private readonly IPopupManager _popupManager;
        private readonly CurrencyHintPopupPresenterFactory _popupPresenterFactory;
        private readonly ILocalization _locale;

        public CurrencyPresenterFactory(
            CurrencyHintPopupPresenterFactory popupPresenterFactory,
            IPopupManager popupManager,
            ILocalization locale)
        {
            _popupPresenterFactory = popupPresenterFactory;
            _popupManager = popupManager;
            _locale = locale;
        }

        public CurrencyPresenter Create(CurrencyCell model)
        {
            return new CurrencyPresenter(model, _locale, _popupManager, _popupPresenterFactory);
        }
    }
}