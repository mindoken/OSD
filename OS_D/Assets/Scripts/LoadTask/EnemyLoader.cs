using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public sealed class EnemyLoader
    {
        
        private readonly IEnemyFactory enemyFactory;

        public EnemyLoader(IEnemyFactory enemyFactory) 
        {
            this.enemyFactory = enemyFactory;
        }

        public void Load(List<Vector2> positions)
        {
            enemyFactory.Load();

            foreach (var position in positions)
            {
                enemyFactory.Create(position);
            }
        }

    }
}