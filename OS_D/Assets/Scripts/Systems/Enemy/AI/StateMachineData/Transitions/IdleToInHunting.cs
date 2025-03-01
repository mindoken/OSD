

using Zenject;

namespace Game.Enemy.AI
{
    public sealed class IdleToInHunting : ITransition, IInitializable
    {
        private readonly DiContainer diContainer;

        private int level;
        private IState targetState;

        private ICondition isDetect;
        public IdleToInHunting(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }


        void IInitializable.Initialize()
        {
            this.level = 0;
            this.targetState = diContainer.Resolve<InHunting>();
            //Resolve conditions
            this.isDetect = diContainer.Resolve<IsDetect>();
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
            return isDetect.Test();
        }
    }
}