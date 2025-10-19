using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public interface ICurrencyPresenter
    {
        ReactiveCommand SpendCurrencyAnimation { get; }
        ReactiveCommand EarnCurrencyAnimation { get; }
        IReadOnlyReactiveProperty<string> Currency { get; }
        Sprite Icon { get; }
        void ShowHintPopup();
        void HideHintPopup();
        void Dispose();
    }
}