using UnityEngine;

namespace Game.Enemy.AI
{
    public sealed class SeePlayer : ICondition
    {
        private readonly EnemyBase enemy;
        private readonly Player player;
        private readonly LayerMask rayTargetMask;

        public SeePlayer(EnemyBase enemy, Player player)
        {
            this.enemy = enemy;
            this.player = player;
            rayTargetMask = LayerMask.GetMask("Wall", "Player");
        }

        bool ICondition.Test()
        {
            Vector2 enemyPosition = this.enemy.transform.position;
            Vector2 playerPosition = this.player.transform.position;
            Vector2 direction = playerPosition - enemyPosition;

            RaycastHit2D raycast = Physics2D.Raycast(enemyPosition, direction, enemy.huntingRange, rayTargetMask);
            if (raycast)
            {
                if (raycast.collider.gameObject.CompareTag("Player"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}