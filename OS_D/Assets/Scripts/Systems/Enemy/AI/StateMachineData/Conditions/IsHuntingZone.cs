namespace Game.Enemy.AI
{
    public sealed class IsHuntingZone : ICondition
    {
        private readonly EnemyBase enemy;
        private readonly Player player;
        public IsHuntingZone(EnemyBase enemy, Player player)
        {
            this.enemy = enemy;
            this.player = player;
        }

        bool ICondition.Test()
        {
            return (this.player.transform.position - this.enemy.transform.position).magnitude <= enemy.huntingRange;
        }
    }
}