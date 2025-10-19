using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class CurrencyEnoughButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyText;
        [SerializeField] private Button _mainButton;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private TMP_Text _buttonBlockedText;
        [SerializeField] private TMP_Text _mainButtonText;

        private ICurrencyEnoughButtonPresenter _presenter;
        private CompositeDisposable _compositeDisposable;

        public void Initialize(ICurrencyEnoughButtonPresenter presenter)
        {
            _presenter = presenter;
        }

        private void OnEnable()
        {
            if (_presenter == null)
                return;
            _compositeDisposable = new();
            _presenter.CurrencyText.Subscribe(Value => _currencyText.text = Value).AddTo(_compositeDisposable);
            _presenter.IsCurrencyEnough.Subscribe(Value => _mainButton.interactable = Value).AddTo(_compositeDisposable);
            _presenter.CurrencyIcon.Subscribe(Value => _currencyIcon.sprite = Value).AddTo(_compositeDisposable);
            _presenter.BlockingButtonText.Subscribe(Value => _buttonBlockedText.text = Value).AddTo(_compositeDisposable);
            _presenter.IsButtonBlocked.Subscribe(Value => OnBlockedButtonStateChanged(Value)).AddTo(_compositeDisposable);
            _presenter.MainButtonText.Subscribe(Value => SetMainButtonText(Value)).AddTo(_compositeDisposable);
            _mainButton.onClick.AddListener(_presenter.ClickButton);
        }

        private void SetMainButtonText(string text)
        {
            if (text == null)
            {
                _mainButtonText.gameObject.SetActive(false);
            }
            else
            {
                _mainButtonText.gameObject.SetActive(true);
                _mainButtonText.text = text;
            }
        }

        private void OnBlockedButtonStateChanged(bool isBlocking)
        {
            if (isBlocking)
            {
                _currencyIcon.enabled = false;
                _currencyText.enabled = false;
                _mainButton.interactable = false;
                _buttonBlockedText.enabled = true;
                _mainButtonText.enabled = false;
            }
            else
            {
                _currencyIcon.enabled = true;
                _currencyText.enabled = true;
                _mainButtonText.enabled = true;
                _mainButton.interactable = _presenter.IsCurrencyEnough.Value;
                _buttonBlockedText.enabled = false;
            }
        }

        private void OnDisable()
        {
            if (_presenter == null)
                return;
            _compositeDisposable?.Dispose();
            _mainButton.onClick.RemoveListener(_presenter.ClickButton);
        }
    }
}