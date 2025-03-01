using System.Collections.Generic;
using Unity.VisualScripting;

namespace Game
{
    public sealed class LevelLoader: IInitializable
    {

        private readonly EnemyLoader enemyLoader;

        public LevelLoader(EnemyLoader enemyLoader)
        {
            this.enemyLoader = enemyLoader;
        }

        void IInitializable.Initialize()
        {
            
        }
    }
}