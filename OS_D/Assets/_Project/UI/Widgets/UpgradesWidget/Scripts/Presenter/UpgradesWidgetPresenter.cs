using Infrastructure;
using MetaGame;
using System.Collections.Generic;

namespace UI
{
    public sealed class UpgradesWidgetPresenter : IUpgradesWidgetPresenter
    {
        public IUpgradePreviewListPresenter UpgradePreviewListPresenter { get; private set; }
        public IUpgradeViewPresenter UpgradeViewPresenter { get; private set; }

        public UpgradesWidgetPresenter(
            List<Upgrade> upgrades,
            CurrencyBank bank,
            UpgradesManager manager,
            ILocalization localization)
        {
            UpgradeViewPresenter = new UpgradeViewPresenter(upgrades[0], bank, manager, localization);
            UpgradePreviewListPresenter = new UpgradePreviewListPresenter(upgrades, bank, UpgradeViewPresenter, localization);
        }

        public void Dispose()
        {
            UpgradePreviewListPresenter.Dispose();
            UpgradeViewPresenter.Dispose();
        }
    }
}