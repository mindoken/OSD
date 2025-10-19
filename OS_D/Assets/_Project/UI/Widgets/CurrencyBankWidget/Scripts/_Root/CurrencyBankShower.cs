using Infrastructure;
using MetaGame;
using System.Collections.Generic;

namespace UI
{
    public sealed class CurrencyBankShower : WidgetShower<ICurrencyBankWidgetPresenter, CurrencyBankWidget>
    {
        private readonly CurrencyBank _bank;
        private readonly CurrencyName[] _config;
        private readonly IPopupManager _popupManager;
        private readonly ILocalization _locale;

        public CurrencyBankShower(
            CurrencyBankWidget_Pipeline pipeline,
            CurrencyBank bank,
            IPopupManager popupManager,
            ILocalization locale) : base(pipeline.Prefab, pipeline.Name, pipeline.ShowOnStart)
        {
            _config = pipeline.Cells;
            _bank = bank;
            _popupManager = popupManager;
            _locale = locale;
        }

        protected override ICurrencyBankWidgetPresenter CreatePresenter()
        {
            List<CurrencyCell> cells = new();
            foreach (var key in _config)
            {
                 cells.Add(_bank.GetCell(key));
            }

            var popupPresenterFactory = new CurrencyHintPopupPresenterFactory(_bank, _locale);
            var currencyPresenterFactory = new CurrencyPresenterFactory(popupPresenterFactory, _popupManager, _locale);

            return new CurrencyBankWidgetPresenter(cells, currencyPresenterFactory);
        }
    }
}