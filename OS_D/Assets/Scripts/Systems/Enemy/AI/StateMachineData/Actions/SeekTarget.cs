
using UnityEngine;

namespace Game.Enemy.AI.Actions
{
    public class SeekTarget : IAction
    {
        private readonly Transform target;
        private readonly EnemyBase enemy;

        public SeekTarget(EnemyBase enemy, Transform target)
        {
            this.enemy = enemy;
            this.target = target;
        }

        void IAction.OnAction()
        {
            Vector2 direction = (this.target.position - this.enemy.transform.position).normalized;
            this.enemy.velocity = direction * this.enemy.maxSpeed;
        }

        void IAction.OnEntryAction()
        {
            this.enemy.velocity = Vector2.zero;
        }

        void IAction.OnExitAction()
        {

        }

    }
}