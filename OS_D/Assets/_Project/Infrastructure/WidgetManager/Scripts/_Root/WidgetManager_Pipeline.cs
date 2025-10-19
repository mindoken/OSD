using Alchemy.Inspector;
using App;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(
        fileName = "WidgetManager_Pipeline",
        menuName = "Infrastructure/WidgetManager/New Pipeline"
    )]
    public sealed class WidgetManager_Pipeline : ScriptableObject
    {
        public ScreenName MainScreenName;

        [Title("User Interface Screens")]
        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = false,
            Reorderable = false)]
        public ScreenConfig[] Screens;
    }

    [Serializable]
    [Group]
    public sealed class ScreenConfig
    {
        public ScreenName Screen;

        [ListViewSettings(
            ShowAlternatingRowBackgrounds = AlternatingRowBackground.All,
            ShowFoldoutHeader = true,
            Reorderable = false)]
        public WidgetName[] Composites;
    }
}