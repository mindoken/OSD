using Alchemy.Inspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace Application
{

    [Serializable]
    [Group]
    public abstract class Setting<T>
    {
        [HideInInspector] public ReactiveProperty<T> Value = new();
        [HorizontalGroup("Group1")][HideLabel] public SettingName Name;
        [HorizontalGroup("Group1")][HideLabel] public T InitValue;
    }

    [Serializable]
    public sealed class BoolSetting : Setting<bool> 
    {
        [HorizontalGroup("Group1")] public FasterTag FasterTag;
    }

    [Serializable]
    public sealed class FloatSetting : Setting<float>
    {
        [LabelText("Min")][HorizontalGroup("Group2")] public float MinValue;
        [LabelText("Max")][HorizontalGroup("Group2")] public float MaxValue;
        [LabelText("Whole Numbers")][HorizontalGroup("Group2")] public bool WholeNumbers;
    }

    [Serializable]
    public sealed class EnumSetting : Setting<int>
    {
        [HorizontalGroup("Group1")][HideLabel] public string EnumTypeName;
        [HorizontalGroup("Group1")] public FasterTag FasterTag;

        public IReadOnlyList<EnumLocaleKey> Entries => _entries;

        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            Reorderable = false,
            ShowAddRemoveFooter = false,
            ShowBorder = false,
            ShowBoundCollectionSize = false,
            ShowFoldoutHeader = false)]
        [HorizontalGroup("Group2")][SerializeField] private List<EnumLocaleKey> _entries = new List<EnumLocaleKey>();

        public string GetEntryString(int value)
        {
            for (int i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].NumericValue == value)
                    return _entries[i].EnumValue; 
            }
            return null;
        }

        public void MoveLeft()
        {
            var index = GetEntryIndex(Value.Value);
            var entriesSize = _entries.Count;
            if (index == 0)
                index = entriesSize - 1;
            else
                index = index - 1;
            Value.Value = _entries[index].NumericValue;
        }

        public void MoveRight()
        {
            var index = GetEntryIndex(Value.Value);
            var entriesSize = _entries.Count;
            if (index == entriesSize - 1)
                index = 0;
            else
                index = index + 1;
            Value.Value = _entries[index].NumericValue;
        }

        public int GetEntryIndex(int value)
        {
            for (int i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].NumericValue == value)
                    return i;
            }
            return -1;
        }

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (string.IsNullOrEmpty(EnumTypeName))
            {
                _entries.Clear();
                return;
            }

            Type enumType = GetEnumType(EnumTypeName);

            if (enumType == null || !enumType.IsEnum)
            {
                Debug.LogWarning($"Тип {EnumTypeName} не найден или не является Enum");
                return;
            }

            Array enumValues = Enum.GetValues(enumType);
            if (enumValues.Length == 0)
                return;

            List<EnumLocaleKey> newEntries = new List<EnumLocaleKey>();

            foreach (var value in enumValues)
            {
                Enum enumValue = (Enum)value;
                int numericValue = Convert.ToInt32(value);

                EnumLocaleKey existingEntry = _entries.FirstOrDefault(entry =>
                    entry.EnumValue != null && entry.EnumValue.Equals(enumValue));

                EnumLocaleKey entry = existingEntry ?? new EnumLocaleKey();
                entry.EnumValue = enumValue.ToString();
                entry.NumericValue = numericValue;

                newEntries.Add(entry);
            }

            _entries = newEntries;
        }

        private Type GetEnumType(string enumType)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(enumType);
                if (type == null)
                    continue;
                if (type.IsEnum)
                    return type;
            }
            return null;
        }
#endif
    }

    [Serializable]
    [HorizontalGroup]
    public sealed class EnumLocaleKey
    {
        [HideLabel][ReadOnly] public string EnumValue;
        [HideLabel][ReadOnly] public int NumericValue;
    }

    public enum FasterTag
    {
        None = -1,
        False = 0,
        True = 1,
        Min = 2,
        Max = 3
    }
}