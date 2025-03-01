namespace Game.Enemy.AI
{
    public class Decision : IDecisionTreeNode
    {
        public readonly IDecisionTreeNode trueNode;
        public readonly IDecisionTreeNode falseNode;

        public Decision(IDecisionTreeNode trueNode, IDecisionTreeNode falseNode) 
        {
            this.trueNode = trueNode;
            this.falseNode = falseNode;
        }

        public virtual bool Test() {  return false; }

    }
}