using CoreGame;
using DG.Tweening;
using UnityEngine;

namespace Common
{
    public sealed class BounceScaleAnimation : MonoBehaviour, IAnimation
    {
        [SerializeField] private Transform _view;

        [SerializeField] private AnimationMode _animtaionMode;
        [SerializeField] private float _bounceTime;
        [SerializeField] private Vector3 _targetScale;

        public void Play()
        {
            if (_animtaionMode == AnimationMode.LowPriority && DOTween.IsTweening(_view))
                return;
            if (_animtaionMode == AnimationMode.Override && DOTween.IsTweening(_view))
                DOTween.Kill(_view);

            var startScale = _view.localScale;
            _view.DOScale(_targetScale, _bounceTime).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                _view.DOScale(startScale, _bounceTime);
            });
        }
    }
}