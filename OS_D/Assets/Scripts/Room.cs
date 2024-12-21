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

        // Инициализация макета комнаты стенами или пустым пространством
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Layout[y, x] = ' '; // Пустое пространство
            }
        }

        // Пример создания визуальных стен
        CreateWalls();
    }

    private void CreateWalls()
    {
        Debug.Log("Creating walls for the room...");

        // Пример создания стен (здесь можно настроить логику создания стен)
        for (int y = 0; y < Layout.GetLength(0); y++)
        {
            for (int x = 0; x < Layout.GetLength(1); x++)
            {
                if (x == 0 || x == Layout.GetLength(1) - 1 || y == 0 || y == Layout.GetLength(0) - 1) // Стены по краям
                {
                    Layout[y, x] = '#'; // '#' обозначает стену
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.position = new Vector3(Position.x + x, 0, Position.y + y);
                    wall.transform.localScale = new Vector3(1, 1, 1); // Установите размер куба
                    Debug.Log($"Wall created at: {Position.x + x}, {Position.y + y}");
                }
            }
        }
    }
}