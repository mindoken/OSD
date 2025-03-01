

using Zenject;

namespace Game.Enemy.AI
{
    public sealed class SeekPlayer : State, IInitializable
    {
        private readonly DiContainer diContainer;
        public SeekPlayer(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        void IInitializable.Initialize()
        {
            this.stateName = EnemyState.SEEKPLAYER;
            this.parent = diContainer.Resolve<InHunting>();

            OnBindActions();
            OnBindTransitions();
        }

        private void OnBindActions()
        {
            this.actions.Add(diContainer.Resolve<Actions.SeekPlayer>());
        }

        private void OnBindTransitions()
        {
            this.transitions.Add(diContainer.Resolve<SeekPlayerToHunting>());
        }

    }
}