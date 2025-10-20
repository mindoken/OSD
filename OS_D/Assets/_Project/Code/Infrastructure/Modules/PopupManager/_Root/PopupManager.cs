using App;
using System.Collections.Generic;

namespace Infrastructure
{
    public sealed class PopupManager : IPopupManager
    {
        private readonly Dictionary<PopupName, IPopupShower> _popups = new();

        private IPopupShower _currentPopup;

        private readonly BoolSetting _hintEnabledSetting;

        public PopupManager(
            IPopupShower[] showers,
            GlobalSettings globalSettings)
        {
            for (int i = 0; i < showers.Length; i++)
            {
                var shower = showers[i];
                _popups.Add(shower.PopupName, shower);
            }

            _hintEnabledSetting = globalSettings.GetBoolSetting(SettingName.Hint);
        }

        public void ShowPopup(PopupName name, IPopupPresenter presenter)
        {
            if (name == PopupName.Hint && _hintEnabledSetting.Value.Value == false)
                return;

            if (_currentPopup != null)
                _currentPopup.Hide();

            var popup = _popups[name];
            popup.Show(presenter);
            _currentPopup = popup;
        }

        public void HideCurrentPopup()
        {
            if (_currentPopup == null)
                return;

            _currentPopup.Hide();
            _currentPopup = null;
        }
    }
}