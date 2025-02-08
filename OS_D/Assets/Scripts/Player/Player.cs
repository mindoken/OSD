using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStats stats;
    public float maxHealth = 100f;
    private float currentHealth;

    // Коэффициенты урона
    public float damageType1Coefficient = 1.0f;
    public float damageType2Coefficient = 1.5f;

    private void Start()
    {
        currentHealth = maxHealth;
        stats.ResetStats();
    }

    private void Update()
    {
        // Обновляем время прохождения уровня
        stats.levelTime += Time.deltaTime;
    }

    public void TakeDamage(float amount, int damageType)
    {
        float damage = amount;

        // Применяем коэффициент урона в зависимости от типа
        if (damageType == 1)
        {
            damage *= damageType1Coefficient;
        }
        else if (damageType == 2)
        {
            damage *= damageType2Coefficient;
        }

        currentHealth -= damage;

        // Проверяем, не упал ли игрок
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        stats.deaths++;
        currentHealth = maxHealth; // Восстанавливаем здоровье после смерти
        // Здесь можно добавить логику для перезапуска уровня или появления на контрольной точке
    }

    public void DealDamage(int damageType, int damageAmount)
    {
        // Увеличиваем общий урон
        stats.totalDamage += damageAmount;

        // Здесь можно добавить логику для нанесения урона противникам
    }
}
