
using UnityEngine;

namespace Game.Enemy
{
    public sealed class EnemyDetection : IGameFixedTickable
    {
        private readonly EnemyBase enemy;
        private readonly Player player;
        private readonly LayerMask rayTargetMask;
        public EnemyDetection(EnemyBase enemy, Player player)
        {
            this.enemy = enemy;
            this.player = player;
            rayTargetMask = LayerMask.GetMask("Wall", "Player");
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            if (!this.enemy.detect)
            {
                Vector2 enemyPosition = this.enemy.transform.position;
                Vector2 playerPosition = this.player.transform.position;
                Vector2 direction = playerPosition - enemyPosition;
                float detectRange = this.enemy.detectRange;
                float distance = direction.magnitude;

                if (distance <= detectRange)
                {
                    RaycastHit2D raycast = Physics2D.Raycast(enemyPosition, direction, detectRange, rayTargetMask);
                    if (raycast)
                    {
                        if (raycast.collider.gameObject.CompareTag("Player"))
                        {
                            this.enemy.detect = true;
                        }
                    }
                }
            }
        }
    }
}