using MetaGame;
using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public interface ICurrencyEnoughButtonPresenter
    {
        event Action OnButtonClicked;
        IReadOnlyReactiveProperty<Sprite> CurrencyIcon { get; }
        IReadOnlyReactiveProperty<string> CurrencyText { get; }
        IReadOnlyReactiveProperty<bool> IsCurrencyEnough { get; }
        IReadOnlyReactiveProperty<bool> IsButtonBlocked { get; }
        IReadOnlyReactiveProperty<string> BlockingButtonText { get; }
        IReadOnlyReactiveProperty<string> MainButtonText { get; }
        void SetNewCurrencyData(CurrencyData model);
        void SetNewButtonBlockedText(string key);
        void ClickButton();
        void Dispose();
    }
}