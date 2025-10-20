using Infrastructure;
using System.Collections.Generic;

namespace UI
{
    public interface ICurrencyBankWidgetPresenter : IWidgetPresenter
    {
        IReadOnlyList<ICurrencyPresenter> CurrencyPresenters { get; }
    }
}