using Alchemy.Inspector;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MetaGame
{
    [Serializable]
    public sealed class CustomTable : IValueTable
    {
        [Space]
        [SerializeField]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = true,
            ShowAddRemoveFooter = false,
            Reorderable = false)]
        [NamedArray("Level")]
        private double[] _levels;

        public double GetValue(int level)
        {
            level = Mathf.Clamp(level, 0, _levels.Length - 1);
            return _levels[level];
        }

        public void OnValidate(int maxLevel)
        {
            EvaluatePriceTable(maxLevel);
        }

        private void EvaluatePriceTable(int maxLevel)
        {
            maxLevel += 1;

            if (_levels == null)
            {
                _levels = new double[maxLevel];
                return;
            }

            int currentLength = _levels.Length;

            if (currentLength < maxLevel)
            {
                // Увеличиваем массив
                double[] newArray = new double[maxLevel];
                Array.Copy(_levels, newArray, currentLength);

                // Заполняем новые элементы (например, нулями или другим значением)
                for (int i = currentLength; i < maxLevel; i++)
                {
                    newArray[i] = 0;
                }

                _levels = newArray;
            }
            else if (currentLength > maxLevel)
            {
                // Уменьшаем массив
                double[] newArray = new double[maxLevel];
                Array.Copy(_levels, newArray, maxLevel);
                _levels = newArray;
            }
        }
    }
}