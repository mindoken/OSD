using Infrastructure;
using UnityEngine;
using Common;

namespace UI
{
    public sealed class GlobalSettingsPanel : MonoWidget<IGlobalSettingsPanelPresenter>
    {
        [SerializeField] private BoolSettingView _boolSettingPrefab;
        [SerializeField] private EnumSettingView _enumSettingPrefab;
        [SerializeField] private FloatSettingView _floatSettingPrefab;
        [SerializeField] private SimpleTitleView _titlePrefab;

        [SerializeField] private Transform _container;

        private MemoryPool<BoolSettingView> _boolSettingPool;
        private MemoryPool<EnumSettingView> _enumSettingPool;
        private MemoryPool<FloatSettingView> _floatSettingPool;
        private MemoryPool<SimpleTitleView> _titlePool;

        private IGlobalSettingsPanelPresenter _presenter;

        private void Awake()
        {
            _boolSettingPool = new(_boolSettingPrefab, _container);
            _enumSettingPool = new(_enumSettingPrefab, _container);
            _floatSettingPool = new(_floatSettingPrefab, _container);
            _titlePool = new(_titlePrefab, _container);
        }

        public override void Initialize(IGlobalSettingsPanelPresenter presenter)
        {
            _presenter = presenter;
            for (int i = 0; i < presenter.SettingGroups.Count; i++)
            {
                var settingGroup = presenter.SettingGroups[i];

                var title = _titlePool.SpawnItem();
                title.gameObject.SetActive(false);
                title.Initialize(settingGroup.TitlePresenter);
                title.gameObject.SetActive(true);

                for (int j = 0; j < settingGroup.SettingPresenters.Count; j++)
                {
                    var settingPresenter = settingGroup.SettingPresenters[j];
                    if (settingPresenter is IEnumSettingPresenter enumPresenter)
                    {
                        var enumView = _enumSettingPool.SpawnItem();
                        enumView.gameObject.SetActive(false);
                        enumView.Initialize(enumPresenter);
                        enumView.gameObject.SetActive(true);
                    }
                    else if (settingPresenter is IFloatSettingPresenter floatPresenter)
                    {
                        var floatView = _floatSettingPool.SpawnItem();
                        floatView.gameObject.SetActive(false);
                        floatView.Initialize(floatPresenter);
                        floatView.gameObject.SetActive(true);
                    }
                    else if (settingPresenter is IBoolSettingPresenter boolPresenter)
                    {
                        var boolView = _boolSettingPool.SpawnItem();
                        boolView.gameObject.SetActive(false);
                        boolView.Initialize(boolPresenter);
                        boolView.gameObject.SetActive(true);
                    }
                }
            }
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