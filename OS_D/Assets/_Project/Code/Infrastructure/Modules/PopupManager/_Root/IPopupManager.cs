namespace Infrastructure
{
    public interface IPopupManager
    {
        void ShowPopup(PopupName name, IPopupPresenter presenter);
        void HideCurrentPopup();
    }
}