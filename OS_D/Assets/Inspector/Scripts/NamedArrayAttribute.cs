using UnityEngine;

public class NamedArrayAttribute : PropertyAttribute
{
    public readonly string BaseName;

    public NamedArrayAttribute(string baseName)
    {
        BaseName = baseName;
    }
}