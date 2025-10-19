using UnityEngine;

namespace UI
{
    public sealed class HintPopupPresenter : IHintPopupPresenter
    {
        public string TitleText => _title;
        private readonly string _title;
        public string DescriptionText => _description;
        private readonly string _description;
        public Sprite Icon => _icon;
        private readonly Sprite _icon;

        public HintPopupPresenter(string title, string description, Sprite icon)
        {
            _title = title;
            _description = description;
            _icon = icon;
        }
    }
}