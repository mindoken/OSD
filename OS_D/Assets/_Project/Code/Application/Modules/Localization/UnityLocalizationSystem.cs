using Zenject;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using App;
using UniRx;

namespace Infrastructure
{
    public sealed class UnityLocalizationSystem : ILocalization, IDisposable
    {
        public static readonly string MAIN_STRING_TABLE = "MainString";
        private readonly string[] CURRENCY_SUFFIXES = { "K", "M", "B", "T" };

        public event Action OnLocaleChanged;

        private readonly LocalizedString _localizedString = new();

        public IReadOnlyList<Locale> AvailableLocales => _availableLocales;
        private readonly List<Locale> _availableLocales = LocalizationSettings.AvailableLocales.Locales;

        public int CurrentLocalIndex => _currentLocalIndex;
        private int _currentLocalIndex;
        private string[] _allLocaleNamesByIndex;

        private CultureInfo _currentCulture;
        private CultureInfo _numberCulture;

        private readonly GlobalSettings _globalSettings;
        private readonly CompositeDisposable _compositeDisposable = new();

        public UnityLocalizationSystem(GlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;

            _allLocaleNamesByIndex = new string[_availableLocales.Count];
            Locale currentLocale = LocalizationSettings.SelectedLocale;
            for (int i = 0; i < _availableLocales.Count; i++)
            {
                _allLocaleNamesByIndex[i] = GetLocalNameByIndex(i);

                if (currentLocale == _availableLocales[i])
                    _currentLocalIndex = i;
            }

            string cultureCode = currentLocale.Identifier.Code;
            _currentCulture = CultureInfo.GetCultureInfo(cultureCode);

            LocalizationSettings.SelectedLocaleChanged += LocaleChanged;
            _globalSettings.GetEnumSetting(SettingName.NumberFormat).Value
                .Subscribe(value => OnNumberFormatChanged(value)).AddTo(_compositeDisposable);
        }

        public string GetLocalNameByIndex(int index)
        {
            return _availableLocales[index].LocaleName;
        }

        private void LocaleChanged(Locale currentLocale)
        {
            string cultureCode = currentLocale.Identifier.Code;
            _currentCulture = CultureInfo.GetCultureInfo(cultureCode);

            OnLocaleChanged?.Invoke();
        }

        public string LocalizeString(string key)
        {
            _localizedString.TableReference = MAIN_STRING_TABLE;
            _localizedString.TableEntryReference = key;
            return _localizedString.GetLocalizedString();
        }

        private void OnNumberFormatChanged(int format)
        {
            switch ((NumberCultureInfo)format)
            {
                case NumberCultureInfo.YourLanguage:
                    _numberCulture = null;
                    break;
                case NumberCultureInfo.Europe:
                    _numberCulture = CultureInfo.GetCultureInfo("ru-RU");
                    break;
                case NumberCultureInfo.American:
                    _numberCulture = CultureInfo.GetCultureInfo("en-US");
                    break;
            }
            OnLocaleChanged?.Invoke();
        }

        public string LocalizeNumber<T>(T number, int decimalPlaces = 0) where T : struct, IConvertible, IComparable, IFormattable
        {
            var format = $"N{decimalPlaces}";
            var culture = _numberCulture == null ? _currentCulture : _numberCulture;
            switch (number) 
            {
                case long l: return l.ToString(format, culture);
                case int i: return i.ToString(format, culture);
                case float f: return f.ToString(format, culture);
                case double d: return d.ToString(format, culture);
                case decimal m: return m.ToString(format, culture);
            }
            return number.ToString(format, CultureInfo.CurrentCulture);
        }

        public string FormatAndLocalizeCurrency(long currency)
        {
            var amount = (double)currency;

            if (amount < 1000)
                return LocalizeNumber(amount);

            var index = -1;
            while (amount >= 1000)
            {
                amount /= 1000;
                index++;
            }

            if (amount >= 100)
                return LocalizeNumber(amount) + CURRENCY_SUFFIXES[index];
            else if (amount >= 10)
                return LocalizeNumber(amount, 1) + CURRENCY_SUFFIXES[index];
            else
                return LocalizeNumber(amount, 2) + CURRENCY_SUFFIXES[index];
        }

        public string[] GetAllLocalNamesByIndex()
        {
            return _allLocaleNamesByIndex;
        }

        public void SetLocalByIndex(int index)
        {
            _currentLocalIndex = index;
            LocalizationSettings.SelectedLocale = _availableLocales[index];
            OnLocaleChanged?.Invoke();
        }

        public void Dispose()
        {
            LocalizationSettings.SelectedLocaleChanged -= LocaleChanged;
            _compositeDisposable.Dispose();
        }
    }
}