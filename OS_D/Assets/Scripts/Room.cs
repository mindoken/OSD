using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Rect Bounds;
    public List<Vector2> Doors;
    public List<Room> Connections;

    void Start()
    {
        // ������������� Rect Bounds
        Bounds = new Rect(transform.position, transform.localScale);
        // ������������� Doors
        // ... (�������� ��� ��� ������� ������� ������)

        // ������������� Connections
        // ... (�������� ��� ��� ���������� ������ ������)
    }

}
