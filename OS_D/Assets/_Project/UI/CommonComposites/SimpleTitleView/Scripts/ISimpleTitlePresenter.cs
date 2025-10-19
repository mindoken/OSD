using UniRx;

namespace UI
{
    public interface ISimpleTitlePresenter
    {
        IReadOnlyReactiveProperty<string> Title { get; }
        void Dispose();
    }
}