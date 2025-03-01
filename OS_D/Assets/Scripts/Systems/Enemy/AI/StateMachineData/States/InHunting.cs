using Zenject;

namespace Game.Enemy.AI
{
    public sealed class InHunting : SubMachineState, IInitializable
    {
        private readonly DiContainer diContainer;
        public InHunting(DiContainer diContainer, Hunting hunting) : base(hunting)
        {
            this.diContainer = diContainer;
        }

        void IInitializable.Initialize()
        {
            this.stateName = EnemyState.IN_HUNTING;
            this.parent = null;

            OnBindActions();
            OnBindTransitions();
        }

        private void OnBindActions()
        {
            //PASS
        }

        private void OnBindTransitions()
        {
            this.transitions.Add(diContainer.Resolve<InHuntingToBack>());
        }
    }
}