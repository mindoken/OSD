using MetaGame;
using System;

namespace UI
{
    public interface IUpgradesWidgetPresenter
    {
        IUpgradePreviewListPresenter UpgradePreviewListPresenter { get; }
        IUpgradeViewPresenter UpgradeViewPresenter { get; }
        void Dispose();
    }
}