using Data;
using UI.Presenters;

namespace UI.Views.ResourceView
{
    public interface IResourceView
    {
        void UpdateResourceValue(ResourceId id, int value);
        void InitView(IResourceViewPresenter presenter);
    }
}