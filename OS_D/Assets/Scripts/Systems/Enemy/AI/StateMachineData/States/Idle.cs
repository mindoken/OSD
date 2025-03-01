using Zenject;

namespace Game.Enemy.AI
{
    public sealed class Idle : State, IInitializable
    {
        private readonly DiContainer diContainer;
        public Idle(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        void IInitializable.Initialize()
        {
            this.stateName = EnemyState.IDLE;
            this.parent = null;

            OnBindActions();
            OnBindTransitions();
        }

        private void OnBindActions()
        {

        }

        private void OnBindTransitions()
        {
            transitions.Add(this.diContainer.Resolve<IdleToInHunting>());
        }
    }
}