using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Game
{
    public sealed class TestLevelLoader: IInitializable
    {
        private readonly Transform enemyMarkers;
        private readonly EnemyLoader enemyLoader;

        [Inject]
        public TestLevelLoader(EnemyLoader enemyLoader, Transform enemyMarkers)
        {
            this.enemyLoader = enemyLoader;
            this.enemyMarkers = enemyMarkers;
        }

        void IInitializable.Initialize()
        {
            List<Vector2> enemyPositions = this.enemyMarkers.GetComponentsInChildren<Transform>(true)
                .Where(child => child != this.enemyMarkers)
                .Select(child => new Vector2(child.position.x, child.position.y))
                .ToList();

            enemyLoader.Load(enemyPositions);
        }
       

    }
}