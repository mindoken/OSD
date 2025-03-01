
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Enemy.AI
{
    public class HierarchicalStateMachine : HSMBase
    {
        private readonly IState initialState;
        protected IState currentState;
        public HierarchicalStateMachine(IState initialState)
        {
            this.initialState = initialState;
            //this.currentState = initialState;
        }


        public override List<EnemyState> GetStates()
        {
            if (this.currentState != null) 
            {
                return this.currentState.GetStates();
            }
            return new List<EnemyState>();
        }

        public override UpdateResult OnUpdate()
        {
            UpdateResult result = new UpdateResult();
            result.actions = new List<ActionDelegate>();
            result.transition = null;
            result.level = 0;

            if (this.currentState == null)
            {
                this.currentState = this.initialState;
                result.actions.Add(this.currentState.GetEntryAction);
                return result;
            }

            //
            ITransition triggeredTransition = null;
            foreach (ITransition transition in this.currentState.GetTransitions())
            {
                if (transition.IsTriggered())
                {
                    triggeredTransition = transition;
                    break;
                }
            }

            //
            if (triggeredTransition != null)
            {
                result.transition = triggeredTransition;
                result.level = triggeredTransition.GetLevel();
            }
            else
            {
                result = this.currentState.OnUpdate();
            }

            //
            if (result.transition != null)
            {
                if (result.level == 0)
                {
                    IState targetState = result.transition.GetTargetState();
                    result.actions.Add(this.currentState.GetExitAction);
                    result.actions.Add(result.transition.GetAction);
                    result.actions.Add(targetState.GetEntryAction);

                    this.currentState = targetState;

                    result.actions.Add(this.GetAction);
                    result.transition = null;
                }
                else if (result.level > 0)
                {
                    result.actions.Add(this.currentState.GetExitAction);
                    this.currentState = null;
                    result.level -= 1;
                }
                else
                {
                    IState targetState = result.transition.GetTargetState();
                    SubMachineState targetMachine = targetState.GetParent();
                    result.actions.Add(result.transition.GetAction);
                    List<ActionDelegate> combinedActions = result.actions.Concat(targetMachine.UpdateDown(targetState, -result.level)).ToList();
                    result.actions = combinedActions;
                    result.transition = null;
                }
            }
            else
            {
                result.actions.Add(this.GetAction);
            }
            return result;
        }
    }
}