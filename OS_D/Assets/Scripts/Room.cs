using UnityEngine;

public class Room : MonoBehaviour
{
    public char[,] Layout { get; private set; }
    public (int x, int y) Position { get; private set; }

    public void Initialize(int width, int height, (int x, int y) position)
    {
        Position = position;
        Layout = new char[height, width];

        Debug.Log($"Initializing room at position: {Position.x}, {Position.y} with size: {width}x{height}");

        // ������������� ������ ������� ������� ��� ������ �������������
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Layout[y, x] = ' '; // ������ ������������
            }
        }

        // ������ �������� ���������� ����
        CreateWalls();
    }

    private void CreateWalls()
    {
        Debug.Log("Creating walls for the room...");

        // ������ �������� ���� (����� ����� ��������� ������ �������� ����)
        for (int y = 0; y < Layout.GetLength(0); y++)
        {
            for (int x = 0; x < Layout.GetLength(1); x++)
            {
                if (x == 0 || x == Layout.GetLength(1) - 1 || y == 0 || y == Layout.GetLength(0) - 1) // ����� �� �����
                {
                    Layout[y, x] = '#'; // '#' ���������� �����
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.position = new Vector3(Position.x + x, 0, Position.y + y);
                    wall.transform.localScale = new Vector3(1, 1, 1); // ���������� ������ ����
                    Debug.Log($"Wall created at: {Position.x + x}, {Position.y + y}");
                }
            }
        }
    }
}