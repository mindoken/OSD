
using Game.Enemy.AI;
using Zenject;
using UnityEngine;

namespace Game.Enemy
{
    public sealed class EnemyController: IGameFixedTickable
    {
        private readonly HierarchicalStateMachine stateMachine;
        private readonly EnemyBase enemy;
        public EnemyController(HierarchicalStateMachine stateMachine, EnemyBase enemy)
        {
            this.stateMachine = stateMachine;
            this.enemy = enemy;
        }

        void IGameFixedTickable.FixedTick(float deltaTime) 
        {
            UpdateResult result = stateMachine.OnUpdate();
            foreach (ActionDelegate action in result.actions)
            {
                action.Invoke();
            }
            //this.enemy.enemyState = stateMachine.GetStates(); //TODO: берет на себя х2
        }

    }
}

