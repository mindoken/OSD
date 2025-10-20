using Alchemy.Inspector;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MetaGame
{
    [Serializable]
    public sealed class LinearTable : IValueTable
    {
        [Space]
        [SerializeField]
        private double _startValue;

        [Space]
        [SerializeField]
        private double _stepValue;

        [Space]
        [ReadOnly]
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
            var table = new double[maxLevel + 1];
            table[0] = new double();
            for (var level = 0; level <= maxLevel; level++)
            {
                var value = _startValue + _stepValue * level;
                table[level] = value;
            }

            _levels = table;
        }
    }
}