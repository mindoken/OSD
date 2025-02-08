using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    private void Start()
    {
        // Загрузка статистики при старте игры
        LoadStats();
    }

    public void SaveStats(float levelTime, int totalDamage, int deaths)
    {
        PlayerPrefs.SetFloat("LevelTime", levelTime);
        PlayerPrefs.SetInt("TotalDamage", totalDamage);
        PlayerPrefs.SetInt("Deaths", deaths);
        PlayerPrefs.Save(); // Сохраняем изменения
    }

    public void LoadStats()
    {
        float levelTime = PlayerPrefs.GetFloat("LevelTime", 0f);
        int totalDamage = PlayerPrefs.GetInt("TotalDamage", 0);
        int deaths = PlayerPrefs.GetInt("Deaths", 0);

        Debug.Log($"Level Time: {levelTime}, Total Damage: {totalDamage}, Deaths: {deaths}");
    }
}
