using UnityEngine;

namespace UI
{
    public sealed class UpgradesWidget : MonoBehaviour
    {
        [SerializeField] private UpgradePreviewList _previewList;
        [SerializeField] private UpgradeView _upgradeView;

        private IUpgradesWidgetPresenter _presenter;

        public void Initialize(IUpgradesWidgetPresenter presenter)
        {
            _presenter = presenter;
            _previewList.Initialize(_presenter.UpgradePreviewListPresenter);
            _upgradeView.Initialize(_presenter.UpgradeViewPresenter);
        }

        private void OnEnable()
        {
            if (_presenter == null)
                return;
        }

        private void OnDisable()
        {
            if (_presenter == null)
                return;
        }
    }
}