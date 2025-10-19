using UnityEngine;

namespace Infrastructure
{
    public abstract class MonoWidget<TPresenter> : MonoBehaviour
        where TPresenter : IWidgetPresenter
    {
        public abstract void Initialize(TPresenter presenter);
    }
}