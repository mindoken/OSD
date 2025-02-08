using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStats stats;
    public float maxHealth = 100f;
    private float currentHealth;

    // ������������ �����
    public float damageType1Coefficient = 1.0f;
    public float damageType2Coefficient = 1.5f;

    private void Start()
    {
        currentHealth = maxHealth;
        stats.ResetStats();
    }

    private void Update()
    {
        // ��������� ����� ����������� ������
        stats.levelTime += Time.deltaTime;
    }

    public void TakeDamage(float amount, int damageType)
    {
        float damage = amount;

        // ��������� ����������� ����� � ����������� �� ����
        if (damageType == 1)
        {
            damage *= damageType1Coefficient;
        }
        else if (damageType == 2)
        {
            damage *= damageType2Coefficient;
        }

        currentHealth -= damage;

        // ���������, �� ���� �� �����
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        stats.deaths++;
        currentHealth = maxHealth; // ��������������� �������� ����� ������
        // ����� ����� �������� ������ ��� ����������� ������ ��� ��������� �� ����������� �����
    }

    public void DealDamage(int damageType, int damageAmount)
    {
        // ����������� ����� ����
        stats.totalDamage += damageAmount;

        // ����� ����� �������� ������ ��� ��������� ����� �����������
    }
}
