namespace Game.Enemy.AI
{
    public class TargetNode : IDecisionTreeNode
    {
        public virtual void GetAction() { }
        public virtual IState GetTargetState() { return null; }
        
        public virtual int GetLevel() { return 0; }
    }
}