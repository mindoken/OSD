using Microsoft.Unity.VisualStudio.Editor;
using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    public interface IUpgradePreviewPresenter
    {
        Sprite UpgradeIcon { get; }
        IReadOnlyReactiveProperty<string> UpgradeTitle { get; }
        IReadOnlyReactiveProperty<string> UpgradeLevel { get; }
        IReadOnlyReactiveProperty<Sprite> CurrencyIcon { get; }
        IReadOnlyReactiveProperty<string> CurrencyAmount { get; }
        IReadOnlyReactiveProperty<bool> IsHidden { get; }
        IReadOnlyReactiveProperty<bool> IsMaxLevel { get; }
        void OnUpgradeClicked();
        void Dispose();
    }
}