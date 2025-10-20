using Infrastructure;
using System.Collections.Generic;

namespace UI
{
    public interface IGlobalSettingsPanelPresenter : IWidgetPresenter
    {
        IReadOnlyList<SettingGroupPresenter> SettingGroups { get; }
    }

    public sealed class SettingGroupPresenter
    {
        public ISimpleTitlePresenter TitlePresenter;
        public List<ISettingPresenter> SettingPresenters = new();
    }
}