using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public sealed class CurrencyView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text _currencyText;
        [SerializeField] private Image _iconImage;
  
        [SerializeField] private float _animationDuration = 1.0f;
        [SerializeField] private Color _spendColor;
        [SerializeField] private Color _defaultColor;

        private readonly List<Sequence> _animationSequences = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        private ICurrencyPresenter _presenter;

        public void Initialize(ICurrencyPresenter presenter)
        {
            _presenter = presenter;
            _iconImage.sprite = _presenter.Icon;
        }

        private void OnEnable()
        {
            if (_presenter == null)
            {
                return;
            }
            _presenter.Currency.Subscribe(value => _currencyText.text = value).AddTo(_compositeDisposable);
            _presenter.EarnCurrencyAnimation.Subscribe(_ => EarnCurrencyAnimation()).AddTo(_compositeDisposable);
            _presenter.SpendCurrencyAnimation.Subscribe(_ => SpendCurrencyAnimation()).AddTo(_compositeDisposable);
        }

        private void OnDisable()
        {
            if (_presenter == null)
            {
                return;
            }
            _compositeDisposable.Clear();
        }

        private void EarnCurrencyAnimation()
        {
            StopAnimations();
            BounceAnimation();
        }

        private void SpendCurrencyAnimation()
        {
            StopAnimations();
            BounceAnimation();
        }

        private void BounceAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence
                .AppendCallback(() => _animationSequences.Add(sequence))
                .Append(_currencyText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.2f))
                .Append(_currencyText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.4f))
                .OnComplete(() => _animationSequences.Remove(sequence));
        }

        private void StopAnimations()
        {
            foreach (var sequence in _animationSequences)
            {
                sequence.Kill();
            }
            _animationSequences.Clear();
        }

        public void OnPointerEnter(PointerEventData _)
        {
            _presenter.ShowHintPopup();
        }

        public void OnPointerExit(PointerEventData _)
        {
            _presenter.HideHintPopup();
        }
    }
}