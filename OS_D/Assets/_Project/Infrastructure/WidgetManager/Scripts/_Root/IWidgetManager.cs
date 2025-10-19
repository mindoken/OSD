namespace Infrastructure
{
    public interface IWidgetManager
    {
        void ShowScreen(ScreenName name);
        void HideCurrentScreen();
    }
}