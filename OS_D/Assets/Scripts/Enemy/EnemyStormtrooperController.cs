using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBaseController
{
    public override Vector2 GetNewStrafePosition()
    {
        Debug.Log("���������� ����� �������");
        return playerPosition;
    }

    public override void Attack()
    {
        Debug.Log("Attack");
    }
}
