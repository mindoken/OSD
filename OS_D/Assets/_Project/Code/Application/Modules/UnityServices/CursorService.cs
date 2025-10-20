using System;
using UnityEngine;
using Zenject;

namespace App
{
    public sealed class CursorService
    {
        private readonly Texture2D _defaultCursor;
        private readonly Vector2 _defaultHotspot = new Vector2(7f, 0f);
        private readonly CursorMode _mode = CursorMode.Auto;

        public CursorService(Texture2D defaultCursor)
        {
            _defaultCursor = defaultCursor;
            SetDefaultCursor();
        }

        public void SetCursorTexture(Texture2D texture, Vector2 hotSpot)
        {
            Cursor.SetCursor(texture, hotSpot, _mode);
        }

        public void SetDefaultCursor()
        {
            Cursor.SetCursor(_defaultCursor, _defaultHotspot, _mode);
        }
    }
}