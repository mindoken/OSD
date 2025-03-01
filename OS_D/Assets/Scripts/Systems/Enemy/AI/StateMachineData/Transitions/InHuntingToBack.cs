using Zenject;

namespace Game.Enemy.AI
{
    public sealed class InHuntingToBack : ITransition, IInitializable
    {
        private readonly DiContainer diContainer;

        private int level;
        private IState targetState;

        private ICondition isHuntingZone;
        public InHuntingToBack(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }


        void IInitializable.Initialize()
        {
            this.level = 0;
            this.targetState = diContainer.Resolve<Back>();
            //Resolve conditions
            this.isHuntingZone = diContainer.Resolve<IsHuntingZone>();
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
            return !isHuntingZone.Test();
        }
    }
}