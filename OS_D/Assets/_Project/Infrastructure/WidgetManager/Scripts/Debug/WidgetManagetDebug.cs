using Alchemy.Inspector;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public sealed class WidgetManagerDebug : MonoBehaviour
    {
        [Inject] private readonly IWidgetManager _widgetManager;

        [Button]
        public void SetScreen(ScreenName name)
        {
            _widgetManager.ShowScreen(name);
        }

        [Button]
        public void HideCurrentScreen()
        {
            _widgetManager.HideCurrentScreen();
        }
    }
}