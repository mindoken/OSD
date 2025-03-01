using System.Collections.Generic;

namespace Game.Enemy.AI
{
    public interface IState
    {
        public void GetAction();
        public void GetEntryAction();
        public void GetExitAction();
        public List<ITransition> GetTransitions();
        public List<EnemyState> GetStates();
        public SubMachineState GetParent();
        public UpdateResult OnUpdate();
    }
}