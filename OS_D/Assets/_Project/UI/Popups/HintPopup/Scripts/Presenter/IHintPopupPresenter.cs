using Infrastructure;
using UnityEngine;

namespace UI
{
    public interface IHintPopupPresenter : IPopupPresenter
    {
        string TitleText { get; }
        string DescriptionText { get; }
        Sprite Icon { get; }
    }
}