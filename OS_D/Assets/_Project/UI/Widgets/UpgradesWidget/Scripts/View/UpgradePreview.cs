using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class UpgradePreview : MonoBehaviour
    {
        private readonly Color BLACK = new Color(0f, 0f, 0f, 1f);
        private readonly Color DEFAULT = new Color(1f, 1f, 1f, 1f);
        private readonly Color TRANSPARENT = new Color(0f, 0f, 0f, 0f);

        [SerializeField] private Image _icon;
        [SerializeField] private Button _mainButton;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private TMP_Text _currencyText;
        [SerializeField] private TMP_Text _maxText;

        [SerializeField] private Color _hiddenTextColor;
        [SerializeField] private Color _defaultTextColor;

        private IUpgradePreviewPresenter _presenter;
        private readonly CompositeDisposable _compositeDisposable = new();

        public void Initialize(IUpgradePreviewPresenter presenter)
        {
            _presenter = presenter;
        }
         
        private void OnEnable()
        {
            if (_presenter == null)
                return;

            if (_presenter.IsHidden.Value)
                SetHiddenState();
            else if (_presenter.IsMaxLevel.Value)
                SetMaxLevelState();
            else
                SetDefaultState();

            _icon.sprite = _presenter.UpgradeIcon;
            _presenter.UpgradeTitle.Subscribe(Value => _title.text = Value).AddTo(_compositeDisposable);
            _presenter.UpgradeLevel.Subscribe(Value => _level.text = Value).AddTo(_compositeDisposable);
            _presenter.CurrencyAmount.Subscribe(Value => _currencyText.text = Value).AddTo(_compositeDisposable);
            _presenter.CurrencyIcon.Subscribe(Value => SetCurrencyIcon(Value)).AddTo(_compositeDisposable);

            _presenter.IsHidden.Skip(1).Subscribe(Value => { 
                transform.SetAsFirstSibling();
                SetDefaultState();
            }).AddTo(_compositeDisposable);

            _presenter.IsMaxLevel.Skip(1).Subscribe(Value => {
                transform.SetAsLastSibling();
                SetMaxLevelState();
            }).AddTo(_compositeDisposable);

            _mainButton.onClick.AddListener(_presenter.OnUpgradeClicked);
        }

        private void OnDisable()
        {
            if (_presenter == null)
                return;
            _compositeDisposable.Clear();
            _mainButton.onClick.RemoveListener(_presenter.OnUpgradeClicked);
        }

        private void SetCurrencyIcon(Sprite value)
        {
            if (value == null)
            {
                _currencyIcon.color = TRANSPARENT;
                _currencyIcon.sprite = value;
            }
            else
            {
                _currencyIcon.color = DEFAULT;
                _currencyIcon.sprite = value;
            }
        }

        private void SetHiddenState()
        {
            _icon.color = BLACK;
            _currencyText.color = _hiddenTextColor;
            _level.color = _hiddenTextColor;
            _title.color = _hiddenTextColor;
            _maxText.gameObject.SetActive(false);
        }

        private void SetMaxLevelState()
        {
            _maxText.color = _hiddenTextColor;
            _maxText.gameObject.SetActive(true);
            _level.color = _hiddenTextColor;
            _title.color = _hiddenTextColor;
        }

        private void SetDefaultState()
        {
            _maxText.gameObject.SetActive(false);
            _icon.color = DEFAULT;
            _currencyText.color = _defaultTextColor;
            _level.color = _defaultTextColor;
            _title.color = _defaultTextColor;
        }

        public void CheckMaxLevelSibling()
        {
            if (_presenter.IsMaxLevel.Value)
                transform.SetAsLastSibling();
        }

        public void CheckHiddenSibling()
        {
            if (_presenter.IsHidden.Value)
                transform.SetAsLastSibling();
        }
    }
}