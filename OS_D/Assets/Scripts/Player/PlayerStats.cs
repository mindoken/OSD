using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float levelTime; // Время прохождения уровня
    public int totalDamage; // Общий урон, нанесенный противникам
    public int deaths; // Количество смертей

    public void ResetStats()
    {
        levelTime = 0;
        totalDamage = 0;
        deaths = 0;
    }
}
