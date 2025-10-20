using CoreGame;
using VFX;
using Zenject;

namespace MetaGame
{
    public sealed class BeehiveCountUpgrade : Upgrade
    {
        //[Inject] private BeehiveSystem _system;
        //[Inject] private WarningVFXFactory _warningVFX;

        private readonly BeehiveCount_UpgradeConfig _config;

        public BeehiveCountUpgrade(BeehiveCount_UpgradeConfig config) : base(config)
        {
            _config = config;
        }

        protected override void LevelUp(int level)
        {
            //_system.TrySpawnBeehive(_config.beehiveId);
        }

        public override bool CanLevelUp()
        {
            //if (!_system.CanSpawnBeehive())
            //{
            //    _warningVFX.SpawnAndPlayAnimation(WarningMessage.NOT_ENOUGH_SPACE_KEY); //Подумать над этим
            //    return false;
            //}

            return true;
        }
    }
}