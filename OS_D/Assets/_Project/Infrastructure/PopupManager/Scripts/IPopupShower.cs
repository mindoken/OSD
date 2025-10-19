namespace Infrastructure
{
    public interface IPopupShower
    {
        PopupName PopupName { get; }
        void Show(IPopupPresenter presenter);
        void Hide();
    }
}