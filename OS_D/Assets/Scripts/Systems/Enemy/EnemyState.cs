namespace Game.Enemy.AI
{
    public enum EnemyState
    {
        IDLE = 0,
        IN_HUNTING = 1,
        HUNTING = 2,
        SEEKPLAYER = 3,
        CHARGE = 4,
        BACK = 5,
        IN_MELEE_ATTACK = 6,
        IN_RANGE_ATTACK = 7,
        HIT = 8,
        IDLE_RANGE = 9,
        FLEEPLAYER = 10,
        SHOOT = 11,
        STRAFE = 12
    }
}