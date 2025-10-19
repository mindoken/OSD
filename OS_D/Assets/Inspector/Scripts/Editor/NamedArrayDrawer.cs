#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        var namedArray = attribute as NamedArrayAttribute;

        // Получаем индекс элемента из имени свойства (например, "data.Array.data[0]" ? 0)
        string path = property.propertyPath;
        int indexStart = path.LastIndexOf('[') + 1;
        int index = int.Parse(path.Substring(indexStart, path.Length - indexStart - 1));

        // Создаём новый лейбл, например "Level 0", "Level 1" и т.д.
        string newLabel = $"{namedArray.BaseName} {index}";

        // Рисуем поле с новым именем
        EditorGUI.PropertyField(rect, property, new GUIContent(newLabel), true);
    }
}
#endif