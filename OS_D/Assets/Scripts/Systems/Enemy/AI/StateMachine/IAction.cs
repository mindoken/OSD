namespace Game.Enemy.AI
{
    public interface IAction
    {
        public void OnAction();
        public void OnEntryAction();
        public void OnExitAction();
    }
}