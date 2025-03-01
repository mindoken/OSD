using Zenject;

namespace Game.Enemy.AI
{
    public sealed class SeekPlayerToHunting : ITransition, IInitializable
    {
        private readonly DiContainer diContainer;

        private int level;
        private IState targetState;

        private ICondition seePlayer;
        public SeekPlayerToHunting(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }


        void IInitializable.Initialize()
        {
            this.level = 0;
            this.targetState = diContainer.Resolve<Hunting>();
            //Resolve conditions
            this.seePlayer = diContainer.Resolve<SeePlayer>();
        }


        void ITransition.GetAction()
        {
            //PASS
        }

        int ITransition.GetLevel()
        {
            return this.level;
        }

        IState ITransition.GetTargetState()
        {
            return targetState;
        }

        bool ITransition.IsTriggered()
        {
            return !seePlayer.Test();
        }
    }
}