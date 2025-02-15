using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyBase
{
    [Header("Close Combat")]
    [SerializeField] float closeCombatRange = 2f;
    protected override void Attack()
    {
        if (toPlayerDistance <= closeCombatRange) {
            GameManager.Instance.player.TakeDamage(currentDamageInfo);
        }
    }
}
