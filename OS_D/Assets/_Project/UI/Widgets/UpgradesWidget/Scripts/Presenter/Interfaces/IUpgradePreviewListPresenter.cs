using MetaGame;
using System.Collections.Generic;

namespace UI
{
    public interface IUpgradePreviewListPresenter
    {
        List<IUpgradePreviewPresenter> Presenters { get; }
        void Dispose();
    }
}