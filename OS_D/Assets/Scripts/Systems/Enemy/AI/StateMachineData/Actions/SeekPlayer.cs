using UnityEngine;

namespace Game.Enemy.AI.Actions
{
    public sealed class SeekPlayer : SeekTarget
    {
        public SeekPlayer(EnemyBase enemy, Player player) : base(enemy, player.transform)
        {

        }

    }
}