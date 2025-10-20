using App;
using System;
using Zenject;

namespace Infrastructure
{
    public abstract class WidgetShower<TPresenter, TWidget> : IWidgetShower, IInitializable, IDisposable
        where TWidget : MonoWidget<TPresenter>
        where TPresenter : IWidgetPresenter
    {
        protected TWidget _view;
        protected TPresenter _presenter;
        public WidgetName Name => _name;
        private readonly WidgetName _name;
        private readonly TWidget _prefab;
        private readonly bool _showOnStart;

        [Inject] private readonly IPrefabFactory _factory;
        [Inject] private readonly ITransformProvider _transformProvider;

        public WidgetShower(
            TWidget prefab,
            WidgetName name,
            bool showOnStart)
        {
            _prefab = prefab;
            _name = name;
            _showOnStart = showOnStart;
        }

        void IInitializable.Initialize()
        {
            _presenter = CreatePresenter();
            _view = _factory.CreatePrefab<TWidget>(_prefab, _transformProvider.GetTransform(TransformName.CanvasWidgets));
            Hide();
            _view.Initialize(_presenter);
            if (_showOnStart) Show();
        }

        protected abstract TPresenter CreatePresenter();

        public void Show() => _view.gameObject.SetActive(true);
        public void Hide() => _view.gameObject.SetActive(false);

        public void Dispose()
        {
            _presenter.Dispose();
        }
    }
}