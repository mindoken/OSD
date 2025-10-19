using Infrastructure;
using UniRx;

namespace UI
{
    public sealed class SimpleTitlePresenter : ISimpleTitlePresenter
    {
        public IReadOnlyReactiveProperty<string> Title => _title;
        private readonly ReactiveProperty<string> _title = new();

        private readonly ILocalization _locale;
        private readonly string _key;

        public SimpleTitlePresenter(
            ILocalization locale,
            string key)
        {
            _locale = locale;
            _key = key;

            _title.Value = _locale.LocalizeString(_key);
            _locale.OnLocaleChanged += OnLocaleChanged;
        }

        private void OnLocaleChanged()
        {
            _title.Value = _locale.LocalizeString(_key);
        }

        public void Dispose()
        {
            _locale.OnLocaleChanged -= OnLocaleChanged;
        }
    }
}