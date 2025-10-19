using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Application
{
    public sealed class AppLoaderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _progress;
        [SerializeField] private TMP_Text _progressTitle;

        public void SetProgressTitle(string text)
        {
            _progressTitle.text = text;
        }

        public void SetProgress(string text)
        {
            _progress.text = text;
        }

        public void SetSliderMaxValue(int value)
        {
            _slider.maxValue = value;
        }

        public void SetSliderValue(float value)
        {
            _slider.value = value;
        }
    }
}