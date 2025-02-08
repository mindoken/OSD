using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Пример: игрок получает 10 урона
            player.TakeDamage(10f, 1);
        }
    }

    public void AttackPlayer(int damageType, int damageAmount)
    {
        player.DealDamage(damageType, damageAmount);
    }
}
