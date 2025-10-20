using Alchemy.Inspector;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace CoreGame
{
    public sealed class ScaleShowHideAnimation : MonoBehaviour, IAnimation<bool ,float>
    {
        [SerializeField] private Transform _view;

        [SerializeField] private AnimationMode _animtaionMode;
        [SerializeField] private Vector3 _startScale;
        [SerializeField] private Vector3 _endScale;
        [SerializeField] private float _duration; //0.5f

        [Button]
        public void Play(bool show, float delay = 0f)
        {
            if (_animtaionMode == AnimationMode.LowPriority && DOTween.IsTweening(_view))
                return;
            if (_animtaionMode == AnimationMode.Override && DOTween.IsTweening(_view))
                DOTween.Kill(_view);

            if (show == true)
            {
                _view.localScale = _startScale;
                _view.DOScale(_endScale, _duration).SetEase(Ease.InOutQuad).SetDelay(delay);//.OnStart(() =>
                //{
                //    _view.localScale = _startScale;
                //});
            }
            else
            {
                _view.localScale = _endScale;
                _view.DOScale(_startScale, _duration).SetEase(Ease.InOutQuad).SetDelay(delay);//.OnStart(() =>
                //{
                //    _view.localScale = _endScale;
                //});
            }
        }
    }
}
