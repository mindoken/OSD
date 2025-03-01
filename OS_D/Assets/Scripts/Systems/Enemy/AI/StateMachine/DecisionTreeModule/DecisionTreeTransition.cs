namespace Game.Enemy.AI
{
    public class DecisionTreeTransition: ITransition
    {
        private TargetNode targetNode = null;
        private readonly Decision decisionTreeRoot;

        public DecisionTreeTransition(Decision decisionTreeRoot)
        {
            this.decisionTreeRoot = decisionTreeRoot;
        }

        public void GetAction() { this.targetNode.GetAction(); }

        public IState GetTargetState() { return this.targetNode.GetTargetState(); }

        public int GetLevel() { return this.targetNode.GetLevel(); }

        public bool IsTriggered()
        {
            this.targetNode = MakeDecision(this.decisionTreeRoot) as TargetNode;
            return (this.targetNode != null);
        }

        private IDecisionTreeNode MakeDecision(IDecisionTreeNode node)
        {
            if (node == null || node is TargetNode)
            {
                return node;
            }
            else
            {
                Decision decisionNode = node as Decision;
                if (decisionNode.Test()) 
                {
                    return MakeDecision(decisionNode.trueNode);
                }
                else
                {
                    return MakeDecision(decisionNode.falseNode);
                }
            }
        }
    }
}