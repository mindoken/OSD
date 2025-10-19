using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class FloatSettingView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _settingName;
        [SerializeField] private Slider _slider;

        private IFloatSettingPresenter _presenter;
        private readonly CompositeDisposable _compositeDisposable = new();

        public void Initialize(IFloatSettingPresenter presenter)
        {
            _presenter = presenter;
            _slider.minValue = _presenter.MinValue;
            _slider.maxValue = _presenter.MaxValue;
            _slider.wholeNumbers = _presenter.WholeNumbers;
        }

        private void OnEnable()
        {
            if (_presenter == null)
                return;
            _slider.onValueChanged.AddListener(_presenter.OnValueChangedFromSlider);
            _presenter.SettingName.Subscribe(value => _settingName.text = value).AddTo(_compositeDisposable);
            _presenter.FloatValue.Subscribe(value => _slider.SetValueWithoutNotify(value));
        }

        private void OnDisable()
        {
            if (_presenter == null)
                return;
            _compositeDisposable.Clear();
            _slider.onValueChanged.RemoveListener(_presenter.OnValueChangedFromSlider);
        }
    }
}