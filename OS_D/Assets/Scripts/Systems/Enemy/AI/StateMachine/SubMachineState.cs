using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Enemy.AI
{
    public class SubMachineState: HierarchicalStateMachine, IState
    {
        protected EnemyState stateName;
        protected SubMachineState parent = null;
        protected List<IAction> actions = new List<IAction>();
        protected List<ITransition> transitions = new List<ITransition>();

        public SubMachineState(IState initialState) : base(initialState) { }

        public SubMachineState GetParent() { return parent; }
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

        public List<ActionDelegate> UpdateDown(IState state, int level)
        {
            List<ActionDelegate> actions;

            if (level > 0)
            {
                actions = this.GetParent().UpdateDown(this, level - 1);
            }
            else
            {
                actions = new List<ActionDelegate>();
            }

            if (this.currentState != null)
            {
                actions.Add(this.currentState.GetExitAction);
            }
            this.currentState = state;
            actions.Add(state.GetEntryAction);
            return actions;
        }

        public override List<EnemyState> GetStates()
        {
            List<EnemyState> states = new List<EnemyState>() { this.stateName };
            if (this.currentState != null)
            {
                return states.Concat(this.currentState.GetStates()).ToList();
            }
            else
            {
                return states;
            }
        }
    }
}