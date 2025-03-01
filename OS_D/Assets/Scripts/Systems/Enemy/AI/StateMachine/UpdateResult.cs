using System.Collections.Generic;

namespace Game.Enemy.AI
{
    public struct UpdateResult
    {
        public List<ActionDelegate> actions;
        public ITransition transition;
        public int level;
    }
}