using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy.AI
{
    public class State : HSMBase, IState
    {
        protected EnemyState stateName;
        protected SubMachineState parent = null;
        protected List<IAction> actions = new List<IAction>();
        protected List<ITransition> transitions = new List<ITransition>();

        public override void GetAction()
        {
            foreach (IAction action in this.actions)
            {
                action.OnAction();
            }
        }
        public virtual void GetEntryAction() 
        {

            foreach (IAction action in this.actions)
            {
                action.OnEntryAction();
            }

        }
        public virtual void GetExitAction() 
        {
            foreach (IAction action in this.actions)
            {
                action.OnExitAction();
            }
        }
        public virtual List<ITransition> GetTransitions() { return transitions; }
        public virtual SubMachineState GetParent() {return parent; }

        public override List<EnemyState> GetStates()
        {
            return new List<EnemyState> { stateName };
        }
    }
}