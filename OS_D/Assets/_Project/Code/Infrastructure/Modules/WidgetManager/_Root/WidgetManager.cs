using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Infrastructure
{
    public sealed class WidgetManager : IWidgetManager
    {
        private readonly DepthOfField _depth;

        private readonly Dictionary<WidgetName, IWidgetShower> _widgets = new();
        private readonly Dictionary<ScreenName, WidgetName[]> _screens = new();

        private readonly Queue<ScreenName> _screenQueue = new();

        private readonly ScreenName _mainScreenName;
        private ScreenName _currentScreen;

        public WidgetManager(
            IWidgetShower[] widgets,
            WidgetManager_Pipeline pipeline)
        {
            var screens = pipeline.Screens;

            for (int i = 0; i < widgets.Length; i++)
            {
                var widget = widgets[i];
                _widgets.Add(widget.Name, widget);
            }

            for (int i = 0; i < screens.Length; i++)
            {
                var screen = screens[i];
                _screens.Add(screen.Screen, screen.Composites);
            }

            _mainScreenName = pipeline.MainScreenName;
            _currentScreen = _mainScreenName;
            var volume = Camera.main.gameObject.GetComponent<Volume>();
            if (volume.profile.TryGet<DepthOfField>(out var depth))
                _depth = depth;
            UnsetDepthOfField();
        }

        public void ShowScreen(ScreenName name)
        {
            if (name == _mainScreenName)
                return;

            if (name == _currentScreen)
                return;

            if (_currentScreen == _mainScreenName)
            {
                HideScreen(_mainScreenName, new PrivateArgument());
                SetDepthOfField();
            }
            else
            {
                _screenQueue.Enqueue(_currentScreen);
                HideScreen(_currentScreen, new PrivateArgument());
            }

            _currentScreen = name;
            ShowScreen(name, new PrivateArgument());
        }

        public void HideCurrentScreen()
        {
            if (_currentScreen == _mainScreenName)
                return;

            HideScreen(_currentScreen, new PrivateArgument());
            if (_screenQueue.TryDequeue(out var screen))
            {
                _currentScreen = screen;
                ShowScreen(screen, new PrivateArgument());
            }
            else
            {
                _currentScreen = _mainScreenName;
                ShowScreen(_mainScreenName, new PrivateArgument());
                UnsetDepthOfField();
            }
        }

        private void ShowScreen(ScreenName name, PrivateArgument _)
        {
            var widgets = _screens[name];
            for (int i = 0; i < widgets.Length; i++)
            {
                _widgets[widgets[i]].Show();
            }
        }

        private void HideScreen(ScreenName name, PrivateArgument _)
        {
            var widgets = _screens[name];
            for (int i = 0; i < widgets.Length; i++)
            {
                _widgets[widgets[i]].Hide();
            }
        }

        private void SetDepthOfField() => _depth.active = true;
        private void UnsetDepthOfField() => _depth.active = false;

        private struct PrivateArgument { };
    }
}