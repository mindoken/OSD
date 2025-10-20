using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    public sealed class SimpleTitleView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;

        private ISimpleTitlePresenter _presenter;
        private CompositeDisposable _compositeDisposable;

        public void Initialize(ISimpleTitlePresenter presenter)
        {
            _presenter = presenter;
        }

        private void OnEnable()
        {
            if (_presenter == null)
                return;
            _compositeDisposable = new();
            _presenter.Title.Subscribe(value => _title.text = value).AddTo(_compositeDisposable);
        }

        private void OnDisable()
        {
            if (_presenter == null)
                return;
            _compositeDisposable.Dispose();
        }
    }
}