namespace Infrastructure
{
    public interface IWidgetShower
    {
        WidgetName Name { get; }
        void Show();
        void Hide();
    }
}