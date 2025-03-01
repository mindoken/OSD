namespace Game.Enemy.AI
{
    public interface ITransition
    {
        public int GetLevel();
        public bool IsTriggered();
        public IState GetTargetState();
        public void GetAction();
    }
}