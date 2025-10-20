using Alchemy.Inspector;
using DG.Tweening;
using UnityEngine;

namespace CoreGame
{
    public sealed class ScaleShowAndHoldAnimation : MonoBehaviour, IAnimation
    {
        [SerializeField] private Transform _view;

        [SerializeField] private float _showTime; //0.5f
        [SerializeField] private float _holdTime; //2f

        [ShowInInspector][ReadOnly] private int _animationStage = 0;

        [Button]
        public void Play()
        {
            switch (_animationStage)
            {
                case 0:
                    PlayStageOne();
                    break;
                case 2:
                    DOTween.Kill(_view);
                    PlayStageTwo();
                    break;
                case 3:
                    PlayStageOne();
                    break;
            }
        }

        private void PlayStageOne()
        {
            _view.DOScale(1f, _showTime).OnStart(() =>
            {
                _animationStage = 1;
            }).OnComplete(() => PlayStageTwo());
        }

        private void PlayStageTwo()
        {
            _animationStage = 2;
            _view.DOScale(0f, _showTime).SetDelay(_holdTime).OnStart(() =>
            {
                _animationStage = 3;
            }).OnComplete(() => { _animationStage = 0; });
        }
    }
}