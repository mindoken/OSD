using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class HintPopup : MonoPopup<IHintPopupPresenter>
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;

        public override void Show(IHintPopupPresenter presenter)
        {
            gameObject.SetActive(true);
            _title.text = presenter.TitleText;
            _description.text = presenter.DescriptionText;
            _icon.sprite = presenter.Icon;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}