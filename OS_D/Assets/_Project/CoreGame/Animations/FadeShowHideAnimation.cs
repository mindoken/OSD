using Common;
using DG.Tweening;
using UnityEngine;

namespace CoreGame
{
    public sealed class FadeShowHideAnimation : MonoBehaviour, IAnimation<bool>
    {
        [SerializeField] private SpriteRenderer _renderer;

        [SerializeField] private AnimationMode _animtaionMode;
        [SerializeField] private float _duration;

        public void Play(bool show)
        {
            if (_animtaionMode == AnimationMode.LowPriority && DOTween.IsTweening(_renderer))
                return;
            if (_animtaionMode == AnimationMode.Override && DOTween.IsTweening(_renderer))
                DOTween.Kill(_renderer);

            if (show == true)
            {
                _renderer.DOFade(1f, _duration).SetEase(Ease.InOutQuad).OnStart(() =>
                {
                    _renderer.color = new Color(1f, 1f, 1f, 0f);
                    _renderer.enabled = true;
                });
            }
            else
            {
                _renderer.DOFade(0f, _duration).OnComplete(() =>
                {
                    _renderer.enabled = false;
                });
            }
        }
    }
}