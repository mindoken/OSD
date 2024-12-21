using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    public int roomCount = 10; // ���������� ������
    public int spaceWidth = 50; // ������ ������������
    public int spaceHeight = 50; // ������ ������������
    public int minRoomSize = 3; // ����������� ������ �������
    public int maxRoomSize = 7; // ������������ ������ �������
    public Material floorMaterial; // �������� ��� ����
    public Material wallMaterial; // �������� ��� ����

    private int[,] levelMatrix; // ������� ��� ������������� ������
    private List<Rect> roomPositions = new List<Rect>(); // ������ ������� � �������� ������

    private void Start()
    {
        GenerateLevel();
        PrintLevelMatrix(); // ������� ������� � �������
        CreateVisuals();
    }

    private void GenerateLevel()
    {
        levelMatrix = new int[spaceWidth, spaceHeight];

        for (int i = 0; i < roomCount; i++)
        {
            Rect room = GenerateRandomRoom();
            if (IsRoomValid(room))
            {
                roomPositions.Add(room);
                MarkRoomInMatrix(room, 1); // 1 - ���������� �������
            }
        }

        CreateCorridors(); // ������� �������� ����� ���������
        MarkWalls(); // ���������� ����� � �������
    }

    private Rect GenerateRandomRoom()
    {
        int width = Random.Range(minRoomSize, maxRoomSize + 1);
        int height = Random.Range(minRoomSize, maxRoomSize + 1);
        int x = Random.Range(1, spaceWidth - width - 1);
        int y = Random.Range(1, spaceHeight - height - 1);

        return new Rect(x, y, width, height);
    }

    private bool IsRoomValid(Rect room)
    {
        foreach (var existingRoom in roomPositions)
        {
            if (room.Overlaps(existingRoom))
            {
                return false; // ������� �� �������, ������� ������������
            }
        }
        return true; // ������� �������
    }

    private void MarkRoomInMatrix(Rect room, int value)
    {
        for (int x = (int)room.x; x < (int)(room.x + room.width); x++)
        {
            for (int y = (int)room.y; y < (int)(room.y + room.height); y++)
            {
                levelMatrix[x, y] = value;
            }
        }
    }

    private void CreateCorridors()
    {
        for (int i = 0; i < roomPositions.Count - 1; i++)
        {
            var room1 = roomPositions[i];
            var room2 = roomPositions[i + 1];

            Vector2Int start = GetRandomPointInRoom(room1);
            Vector2Int end = GetRandomPointInRoom(room2);

            CreateCorridor(start, end);
        }
    }

    private Vector2Int GetRandomPointInRoom(Rect room)
    {
        int x = Random.Range((int)room.x + 1, (int)(room.x + room.width) - 1);
        int y = Random.Range((int)room.y + 1, (int)(room.y + room.height) - 1);
        return new Vector2Int(x, y);
    }

    private void CreateCorridor(Vector2Int start, Vector2Int end)
    {
        // ��������� ����������� ��� ��������
        if (Random.Range(0, 2) == 0)
        {
            // ������� ��������������, ����� ������������
            for (int x = Mathf.Min(start.x, end.x); x <= Mathf.Max(start.x, end.x); x++)
            {
                levelMatrix[x, start.y] = 1; // 1 - ���������� �������
            }
            for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y++)
            {
                levelMatrix[end.x, y] = 1; // 1 - ���������� �������
            }
        }
        else
        {
            // ������� ������������, ����� ��������������
            for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y++)
            {
                levelMatrix[start.x, y] = 1; // 1 - ���������� �������
            }
            for (int x = Mathf.Min(start.x, end.x); x <= Mathf.Max(start.x, end.x); x++)
            {
                levelMatrix[x, end.y] = 1; // 1 - ���������� �������
            }
        }
    }

    private void MarkWalls()
    {
        for (int x = 0; x < spaceWidth; x++)
        {
            for (int y = 0; y < spaceHeight; y++)
            {
                // ���� ��� ������ ������ � ����� ���� ������� ��� �������, �� ��� �����
                if (levelMatrix[x, y] == 0 && IsAdjacentToRoomOrCorridor(x, y))
                {
                    levelMatrix[x, y] = 2; // 2 - ���������� �����
                }
            }
        }
    }

    private bool IsAdjacentToRoomOrCorridor(int x, int y)
    {
        // ��������� �������� ������
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (Mathf.Abs(dx) != Mathf.Abs(dy)) // ��������� ������ �������������� � ������������ �������� ������
                {
                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && nx < spaceWidth && ny >= 0 && ny < spaceHeight)
                    {
                        if (levelMatrix[nx, ny] == 1) // ���� �������� ������ - ������� ��� �������
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    private void CreateVisuals()
    {
        for (int x = 0; x < spaceWidth; x++)
        {
            for (int y = 0; y < spaceHeight; y++)
            {
                CreateTile(x, y, levelMatrix[x, y]);
            }
        }
    }

    private void CreateTile(int x, int y, int type)
    {
        GameObject tile;

        switch (type)
        {
            case 1: // ��� ��� �������
                tile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                tile.GetComponent<Renderer>().material = floorMaterial;
                tile.transform.rotation = Quaternion.Euler(90, 0, 0); // ������������ Quad ��� 2D
                break;
            case 2: // �����
                tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.GetComponent<Renderer>().material = wallMaterial;
                break;
            default:
                return; // ������ ������
        }

        tile.transform.position = new Vector3(x, 0, y);
        tile.transform.localScale = new Vector3(1, 1, 1); // ���������� ������ ������
    }

    private void PrintLevelMatrix()
    {
        string output = "";
        for (int y = spaceHeight - 1; y >= 0; y--) // ���������� �� ��� Y ��� ����������� �����������
        {
            for (int x = 0; x < spaceWidth; x++)
            {
                output += levelMatrix[x, y] + " "; // ��������� �������� ������
            }
            output += "\n"; // ������� �� ����� ������
        }
        Debug.Log(output); // ������� � �������
    }
}
