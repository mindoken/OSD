using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace App 
{
    public sealed class ScreenResolutionService : IDisposable
    {
        private const float MIN_RESOLUTION = 1280 * 720;

        public IReadOnlyList<Resolution> Resolutions => _resolutions;
        private readonly List<Resolution> _resolutions = new();

        private readonly GlobalSettings _settings;
        private readonly EnumSetting _resolutionSetting;
        private readonly EnumSetting _fullscreenSetting;
        private readonly CompositeDisposable _compositeDisposable;

        public ScreenResolutionService(GlobalSettings settings)
        {
            _settings = settings;
            for (int i = Screen.resolutions.Length - 1; i >= 0; i--)
            {
                var resolution = Screen.resolutions[i];
                if (resolution.width * resolution.height >= MIN_RESOLUTION)
                    _resolutions.Add(resolution);
            }
            _resolutionSetting = _settings.GetEnumSetting(SettingName.ScreenResolution);
            _fullscreenSetting = _settings.GetEnumSetting(SettingName.FullScreen);
            _compositeDisposable = new();
            _resolutionSetting.Value.Subscribe(Value => SetResolution(Value)).AddTo(_compositeDisposable);
            _fullscreenSetting.Value.Subscribe(Value => SetFullscreen(Value)).AddTo(_compositeDisposable);
        }

        private void SetResolution(int index)
        {
            if (index >= _resolutions.Count)
            {
                _resolutionSetting.Value.Value = 0;
                return;
            }

            var resolution = _resolutions[index];
            var fullscreen = (FullScreenMode)_fullscreenSetting.Value.Value;
            Screen.SetResolution(resolution.width, resolution.height, fullscreen, resolution.refreshRateRatio);
        }

        private void SetFullscreen(int fullscreen)
        {
            var resolution = _resolutions[_resolutionSetting.Value.Value];
            Screen.SetResolution(resolution.width, resolution.height, (FullScreenMode)fullscreen, resolution.refreshRateRatio);
        }

        void IDisposable.Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}
