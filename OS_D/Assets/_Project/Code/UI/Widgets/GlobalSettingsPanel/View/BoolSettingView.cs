using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class BoolSettingView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _settingName;
        [SerializeField] private Button _markerButton;
        [SerializeField] private Image _markerOnImage;
        [SerializeField] private TMP_Text _markerStatusText;
        [SerializeField] private GameObject _fasterTag;

        private IBoolSettingPresenter _presenter;
        private readonly CompositeDisposable _compositeDisposable = new();

        public void Initialize(IBoolSettingPresenter presenter)
        {
            _presenter = presenter;
        }

        private void OnEnable()
        {
            if (_presenter == null)
                return;
            _markerButton.onClick.AddListener(_presenter.OnMarkerClicked);
            _presenter.SettingName.Subscribe(value => _settingName.text = value).AddTo(_compositeDisposable);
            _presenter.MarkerStatusText.Subscribe(value => _markerStatusText.text = value).AddTo(_compositeDisposable);
            _presenter.MarkerStatus.Subscribe(value => _markerOnImage.enabled = value).AddTo(_compositeDisposable);
            _presenter.IsFasterTag.Subscribe(value => _fasterTag.SetActive(value)).AddTo(_compositeDisposable);
        }

        private void OnDisable()
        {
            if (_presenter == null)
                return;
            _compositeDisposable.Clear();
            _markerButton.onClick.RemoveListener(_presenter.OnMarkerClicked);
        }
    }
}