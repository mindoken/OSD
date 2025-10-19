using UnityEngine;
using UnityEngine.UI;
using Infrastructure;

namespace UI
{
    public sealed class UpgradePreviewList : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup _layoutGroup;

        [SerializeField] private UpgradePreview _upgradePreviewPrefab;
        [SerializeField] private Transform _container;
        private MemoryPool<UpgradePreview> _memoryPool;
        private void Awake() => _memoryPool = new(_upgradePreviewPrefab, _container);

        public void Initialize(IUpgradePreviewListPresenter presenter)
        {
            _layoutGroup.enabled = false;
            _memoryPool.Clear();

            for (int i = 0; i < presenter.Presenters.Count; i++)
            {
                var previewPresenter = presenter.Presenters[i];
                var preview = _memoryPool.SpawnItem();
                preview.gameObject.SetActive(false);
                preview.Initialize(previewPresenter);
                preview.gameObject.SetActive(true);
            }

            for (int i = 0; i < _memoryPool.ActiveItems.Count; i++)
                _memoryPool.ActiveItems[i].CheckHiddenSibling();

            for (int i = 0; i < _memoryPool.ActiveItems.Count; i++)
                _memoryPool.ActiveItems[i].CheckMaxLevelSibling();

            _layoutGroup.enabled = true;
        }
    }
}