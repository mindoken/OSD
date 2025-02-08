using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float levelTime; // ����� ����������� ������
    public int totalDamage; // ����� ����, ���������� �����������
    public int deaths; // ���������� �������

    public void ResetStats()
    {
        levelTime = 0;
        totalDamage = 0;
        deaths = 0;
    }
}
