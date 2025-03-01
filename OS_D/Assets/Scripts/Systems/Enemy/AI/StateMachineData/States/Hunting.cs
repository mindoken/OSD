using UnityEngine;
using Zenject;

namespace Game.Enemy.AI
{
    public sealed class Hunting : State, IInitializable
    {
        private readonly DiContainer diContainer;
        public Hunting(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }


        void IInitializable.Initialize()
        {
            this.stateName = EnemyState.HUNTING;
            this.parent = diContainer.Resolve<InHunting>();

            OnBindActions();
            OnBindTransitions();
        }

        private void OnBindActions()
        {
            //TODO: Hunting action
        }

        private void OnBindTransitions()
        {
            transitions.Add(diContainer.Resolve<HuntingToSeekPlayer>());
        }
    }
}