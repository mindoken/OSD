using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class UpgradeView : MonoBehaviour
    {
        private readonly Color HIDDEN_COLOR = new(0f, 0f, 0f, 1f);
        private readonly Color DEFAULT_COLOR = new(1f, 1f, 1f, 1f);

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _level;

        [SerializeField] private CurrencyEnoughButton _upgradeButton;

        private IUpgradeViewPresenter _presenter;
        private readonly CompositeDisposable _compositeDisposable = new();

        public void Initialize(IUpgradeViewPresenter presenter)
        {
            _presenter = presenter;
            _upgradeButton.Initialize(_presenter.CurrencyEnoughButtonPresenter);
        }

        private void OnEnable()
        {
            if (_presenter == null)
                return;
            _presenter.UpgradeTitle.Subscribe(Value => _title.text = Value).AddTo(_compositeDisposable);
            _presenter.UpgradeDescription.Subscribe(Value => _description.text = Value).AddTo(_compositeDisposable);
            _presenter.UpgradeLevel.Subscribe(Value => _level.text = Value).AddTo(_compositeDisposable);
            _presenter.UpgradeIcon.Subscribe(Value => _icon.sprite = Value).AddTo(_compositeDisposable);
            _presenter.IsHidden.Subscribe(Value => OnUpgradeHiddenStateChanged(Value)).AddTo(_compositeDisposable);
        }

        private void OnUpgradeHiddenStateChanged(bool isHidden)
        {
            if (isHidden) _icon.color = HIDDEN_COLOR;
            else _icon.color = DEFAULT_COLOR;
        }

        private void OnDisable()
        {
            if (_presenter == null)
                return;
            _compositeDisposable.Clear();
        }
    }
}