using Alchemy.Inspector;
using System;

namespace MetaGame
{
    public abstract class Upgrade
    {
        public event Action<int> OnLevelUp;
        public event Action OnOpened;

        [ReadOnly, ShowInInspector] public string Id => _config.id;
        [ReadOnly, ShowInInspector] public int Level => _currentLevel;
        [ReadOnly, ShowInInspector] public int MaxLevel => _config.maxLevel;
        [ReadOnly, ShowInInspector] public CurrencyData NextPrice => 
            new CurrencyData { key = _config.priceType, amount = (long)_config.priceTable.GetValue(Level + 1) };
        [ReadOnly, ShowInInspector] public UpgradeMetadata Metadata => _config.metadata;

        public bool IsMaxLevel => _currentLevel == _config.maxLevel;
        public bool IsOpen => _isOpen; 
        public float Progress => (float)_currentLevel / _config.maxLevel;

        private readonly UpgradeConfig _config;
        private int _currentLevel;
        private bool _isOpen;

        protected Upgrade(UpgradeConfig config)
        {
            _config = config;
            _currentLevel = config.initialStats.Level;
            _isOpen = config.initialStats.OpenOnStart;
        }

        public void OpenUpgrade()
        {
            if (!_isOpen)
            {
                _isOpen = true;
                OnOpened?.Invoke();
            }
        }

        public void SetupOpen(bool isOpen) => _isOpen = isOpen;
        public void SetupLevel(int level) => _currentLevel = level;

        public void LevelUp()
        {
            if (Level >= MaxLevel)
            {
                throw new Exception($"Can not increment level for upgrade {_config.id}!");
            }

            var nextLevel = Level + 1;
            _currentLevel = nextLevel;
            LevelUp(nextLevel);
            OnLevelUp?.Invoke(nextLevel);
        }

        public virtual bool CanLevelUp()
        {
            return true;
        }

        protected abstract void LevelUp(int level);
    }
}