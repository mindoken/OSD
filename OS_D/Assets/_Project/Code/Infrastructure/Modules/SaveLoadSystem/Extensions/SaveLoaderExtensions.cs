using System;
using UnityEngine;

namespace Infrastructure.SaveLoadExtensions
{
    [Serializable]
    public struct Position
    {
        public float x;
        public float y;
        public float z;

        public Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public static class SaveLoaderExtensions
    {
        public static Vector3 PositionToVector3(Position position)
        {
            return new Vector3(position.x, position.y, position.z);
        }

        public static Position Vector3ToPosition(Vector3 vector)
        {
            return new Position(vector.x, vector.y, vector.z);
        }

        public static string Vector3ToID(Vector3 vector)
        {
            return Mathf.FloorToInt(vector.x * 100).ToString("000") +
                Mathf.FloorToInt(vector.y * 100).ToString("000") +
                Mathf.FloorToInt(vector.z * 100).ToString("000");
        }
    }
}