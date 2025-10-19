using Infrastructure;
using MetaGame;
using System.Collections.Generic;

namespace UI
{
    public sealed class CurrencyBankWidgetPresenter : ICurrencyBankWidgetPresenter
    {
        public IReadOnlyList<ICurrencyPresenter> CurrencyPresenters => currencyPresenters;
        private readonly List<ICurrencyPresenter> currencyPresenters = new();

        public CurrencyBankWidgetPresenter(
            List<CurrencyCell> model,
            CurrencyPresenterFactory presenterFactory)
        {
            foreach (var cell in model)
            {
                this.currencyPresenters.Add(presenterFactory.Create(cell));
            }
        }

        public void Dispose()
        {
            foreach (var presenter in currencyPresenters)
            {
                presenter.Dispose();
            }
        }
    }
}