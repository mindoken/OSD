#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        var namedArray = attribute as NamedArrayAttribute;

        // �������� ������ �������� �� ����� �������� (��������, "data.Array.data[0]" ? 0)
        string path = property.propertyPath;
        int indexStart = path.LastIndexOf('[') + 1;
        int index = int.Parse(path.Substring(indexStart, path.Length - indexStart - 1));

        // ������ ����� �����, �������� "Level 0", "Level 1" � �.�.
        string newLabel = $"{namedArray.BaseName} {index}";

        // ������ ���� � ����� ������
        EditorGUI.PropertyField(rect, property, new GUIContent(newLabel), true);
    }
}
#endif