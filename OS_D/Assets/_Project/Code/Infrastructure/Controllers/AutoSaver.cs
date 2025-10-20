using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using UniRx;
using Infrastructure;
using System;

namespace App
{
    public sealed class AutoSaver : IFixedTickable, IDisposable
    {
        private readonly ISaveLoadSystem _saveLoadSystem;
        private readonly CompositeDisposable _compositeDisposable = new();

        private float _currentTime = 0f;
        private float _goalSaveTime = 1800f;
        private bool _isAutoSave = false;

        public AutoSaver(
            ISaveLoadSystem saveLoadSystem,
            GlobalSettings globalSettings)
        {
            _saveLoadSystem = saveLoadSystem;

            globalSettings.GetEnumSetting(SettingName.AutoSave).Value
                .Subscribe(value => OnAutoSaveSettingChanged(value)).AddTo(_compositeDisposable);

            Application.quitting += OnApplicationQuit;
        }

        private void OnAutoSaveSettingChanged(int value)
        {
            var frecuency = (AutoSaveFrequency)value;
            _currentTime = 0f;
            switch (frecuency)
            {
                case AutoSaveFrequency.OnlyOnExit:
                    _isAutoSave = false;
                    break;
                case AutoSaveFrequency.Per30Minutes:
                    _isAutoSave = true;
                    _goalSaveTime = 1800f;
                    break;
                case AutoSaveFrequency.Per15Minutes:
                    _isAutoSave = true;
                    _goalSaveTime = 900f;
                    break;
                case AutoSaveFrequency.Per5Minutes:
                    _isAutoSave = true;
                    _goalSaveTime = 300f;
                    break;
                case AutoSaveFrequency.Per2Minutes:
                    _isAutoSave = true;
                    _goalSaveTime = 120f;
                    break;
            }
        }

        void IFixedTickable.FixedTick()
        {
            if (!_isAutoSave)
                return;
            _currentTime += Time.fixedDeltaTime;
            if (_currentTime > _goalSaveTime)
            {
                _currentTime = 0f;
                _saveLoadSystem.Save().Forget();
            }
        }

        private void OnApplicationQuit()
        {
            _saveLoadSystem.Save().Forget();
        }

        public void Dispose()
        {
            Application.quitting -= OnApplicationQuit;
            _compositeDisposable.Dispose();
        }
    }
}