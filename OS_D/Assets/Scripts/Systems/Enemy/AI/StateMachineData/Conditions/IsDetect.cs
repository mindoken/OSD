namespace Game.Enemy.AI
{
    public sealed class IsDetect : ICondition
    {
        private readonly EnemyBase enemy;
        public IsDetect(EnemyBase enemy)
        {
            this.enemy = enemy;
        }

        bool ICondition.Test()
        {
            return this.enemy.detect;
        }
    }
}