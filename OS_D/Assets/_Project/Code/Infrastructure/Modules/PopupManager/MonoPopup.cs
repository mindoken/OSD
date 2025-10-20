using UnityEngine;

namespace Infrastructure
{
    public abstract class MonoPopup<TPresenter> : MonoBehaviour
        where TPresenter : IPopupPresenter
    {
        public abstract void Show(TPresenter presenter);
        public abstract void Hide();
    }
}