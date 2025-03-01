
using System.Collections.Generic;

namespace Game.Enemy.AI
{
    public class HSMBase
    {
        public virtual void GetAction() { }
        public virtual UpdateResult OnUpdate()
        {
            UpdateResult result = new UpdateResult();
            result.actions = new List<ActionDelegate> { this.GetAction };
            result.transition = null;
            result.level = 0;
            return result;
        }
        
        public virtual List<EnemyState> GetStates() { return new List<EnemyState>(); }
    }
}