using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public int numRooms = 10;
    public int maxAttempts = 100;

    private List<Room> rooms = new List<Room>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        // �������� ��������� �������
        CreateRoom(new Vector2(-10, -10));
        CreateRoom(new Vector2(10, -10));
        CreateRoom(new Vector2(-10, 10));
        CreateRoom(new Vector2(10, 10));

        // ���������� ��� �������� ��������� ������
        for (int i = 0; i < numRooms - 4; i++) // �������� ������� �����
        {
            // ����� ���������� ����� ��� ����� �������
            Vector2 newRoomPos = FindFreeSpaceForRoom();

            // �������� ����� �������, ���� ���� �����
            if (newRoomPos != Vector2.zero)
            {
                CreateRoom(newRoomPos);
            }
            else
            {
                Debug.LogWarning("�� ������� ����� ����� ��� ����� �������.");
            }
        }
    }

    Vector2 FindFreeSpaceForRoom()
    {
        // ���������� ������� ������ ���������� ����� ��� ����� �������
        // (�������� ����������� � ������������� ���������)
        // ���������� Vector2 � �������� ��� ����� �������, ��� Vector2.zero, ���� ����� ���
        if (rooms.Count > 1)
        {

            for (int i = 0; i < maxAttempts; i++)
            {
                // ��������� ����������� ��� ����� �������
                int direction = Random.Range(0, 4);
                Vector2 offset = Vector2.zero;

                switch (direction)
                {
                    case 0: offset = Vector2.right; break; // ������
                    case 1: offset = Vector2.left; break; // �����
                    case 2: offset = Vector2.up; break; // �����
                    case 3: offset = Vector2.down; break; // ����
                }

                // ��������, �������� �� ����� ��� ����� �������
                bool isFree = true;
                foreach (Room room in rooms)
                {
                    if (room.Bounds.Overlaps(new Rect(room.Bounds.center + offset, room.Bounds.size)))
                    {
                        isFree = false;
                        break;
                    }
                }

                if (isFree)
                {
                    return rooms[-1].Bounds.center + offset;
                }
            }
        }

        return Vector2.zero; // �� ������� ���������� �����
    }

    void CreateRoom(Vector2 position)
    {
        // �������� ����� ������� � ������� Instantiate
        GameObject newRoom = Instantiate(roomPrefab, position, Quaternion.identity);
        Room room = newRoom.GetComponent<Room>();

        // ���������� ������ ������
        rooms.Add(room);
        Debug.Log(rooms);

        if (rooms.Count > 2)
        {
            // �������� �������� ����� ����� � ������ ���������
            CreateCorridor(rooms[rooms.Count], room);

            // ���������� ������ ����� ���������
            // ... (���������� ��� ��� ����������� ������ ����� ���������)
        }
    }
    void CreateCorridor(Room room1, Room room2)
    {
        Vector2 room1Center = room1.Bounds.center;
        Vector2 room2Center = room2.Bounds.center;

        // ����� ���������� ����������� ��� ��������
        int direction = Random.Range(0, 2);
        if (direction == 0)
        {
            // ������� �� �����������
            GameObject corridor = new GameObject("Corridor");
            LineRenderer lineRenderer = corridor.AddComponent<LineRenderer>(); // ������������� LineRenderer

            // ��������� ������� ��� ��������
            lineRenderer.SetPosition(0, room1Center);
            lineRenderer.SetPosition(1, room2Center);
        }
        else
        {
            // ������� �� ���������
            GameObject corridor = new GameObject("Corridor");
            LineRenderer lineRenderer = corridor.AddComponent<LineRenderer>(); // ������������� LineRenderer

            // ��������� ������� ��� ��������
            lineRenderer.SetPosition(0, room1Center);
            lineRenderer.SetPosition(1, room2Center);
        }
    }
}