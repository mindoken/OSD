using Infrastructure;
using MetaGame;
using System.Collections.Generic;

namespace UI
{
    public sealed class UpgradePreviewListPresenter : IUpgradePreviewListPresenter
    {
        public List<IUpgradePreviewPresenter> Presenters { get; private set; } = new();

        private readonly IUpgradeViewPresenter _upgradeViewPresenter;

        public UpgradePreviewListPresenter(
            List<Upgrade> upgrades,
            CurrencyBank bank,
            IUpgradeViewPresenter viewPresenter,
            ILocalization locale)
        {
            _upgradeViewPresenter = viewPresenter;
            for (int i = 0; i < upgrades.Count; i++)
            {
                var presenter = new UpgradePreviewPresenter(upgrades[i], bank, _upgradeViewPresenter, locale);
                Presenters.Add(presenter);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < Presenters.Count; i++)
            {
                Presenters[i].Dispose();
            }
        }
    }
}