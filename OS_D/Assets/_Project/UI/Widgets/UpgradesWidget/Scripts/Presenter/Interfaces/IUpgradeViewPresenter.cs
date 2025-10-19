using MetaGame;
using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public interface IUpgradeViewPresenter
    {
        ICurrencyEnoughButtonPresenter CurrencyEnoughButtonPresenter { get; }
        IReadOnlyReactiveProperty<string> UpgradeTitle { get; }
        IReadOnlyReactiveProperty<string> UpgradeDescription { get; }
        IReadOnlyReactiveProperty<Sprite> UpgradeIcon { get; }
        IReadOnlyReactiveProperty<string> UpgradeLevel { get; }
        IReadOnlyReactiveProperty<bool> IsHidden { get; }
        void SetUpgrade(Upgrade upgrade);
        void Dispose();
    }
}