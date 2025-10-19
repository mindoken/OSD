using Infrastructure;
using UnityEngine;
using Common;

namespace UI
{
    public sealed class CurrencyBankWidget : MonoWidget<ICurrencyBankWidgetPresenter>
    {
        [SerializeField] private CurrencyView _currencyPrefab;
        [SerializeField] private Transform _content;
        private MemoryPool<CurrencyView> _pool;
        private void Awake() => _pool = new(_currencyPrefab, _content);

        public override void Initialize(ICurrencyBankWidgetPresenter presenter)
        {
            for (int i = 0; i < presenter.CurrencyPresenters.Count; i++)
            {
                var currencyPresenter = presenter.CurrencyPresenters[i];
                var view = _pool.SpawnItem();
                view.gameObject.SetActive(false);
                view.Initialize(currencyPresenter);
                view.gameObject.SetActive(true);
            }
        }
    }
}