using Zenject;

namespace Game.Enemy.AI
{
    public sealed class Back : State, IInitializable
    {
        private readonly DiContainer diContainer;
        public Back(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }


        void IInitializable.Initialize()
        {
            this.stateName = EnemyState.BACK;
            this.parent = null;

            OnBindActions();
            OnBindTransitions();
        }

        private void OnBindActions()
        {
            //TODO: BACK action
        }

        private void OnBindTransitions()
        {
            //TODO: to IDLE transition (if Back finished)
        }

    }
}