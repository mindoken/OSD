using App;
using Zenject;

namespace Infrastructure
{
    public sealed class PopupShower<TMonoPopup, TPresenter> : IPopupShower, IInitializable
        where TMonoPopup : MonoPopup<TPresenter>
        where TPresenter : IPopupPresenter
    {
        private TMonoPopup _view;

        public PopupName PopupName => _popupName;
        private readonly PopupName _popupName;

        private readonly IPrefabFactory _prefabFactory;
        private readonly ITransformProvider _transformProvider;
        private readonly TMonoPopup _prefab;

        public PopupShower(
            PopupName popupName,
            TMonoPopup prefab,
            IPrefabFactory prefabFactory,
            ITransformProvider transformProvider)
        {
            _popupName = popupName;
            _prefab = prefab;
            _prefabFactory = prefabFactory;
            _transformProvider = transformProvider;
        }

        public void Initialize()
        {
            _view = _prefabFactory.CreatePrefab<TMonoPopup>(_prefab, _transformProvider.GetTransform(TransformName.CanvasPopups));
            _view.Hide();
        }

        public void Show(IPopupPresenter presenter)
        {
            if (presenter is TPresenter targetPresenter)
                _view.Show(targetPresenter);
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}