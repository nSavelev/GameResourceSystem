using Services.GameResources;
using UI.Presenters;
using UI.Views.ResourceView;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField]
        private ResourceView _resourceView = null;

        private ResourceViewPresenter _resourcePresenter = null;

        public void Init(IGameResourceService resourceService)
        {
            _resourcePresenter = new ResourceViewPresenter(resourceService.Data);
            _resourcePresenter.SetView(_resourceView);
        }

        private void OnDestroy()
        {
            _resourcePresenter.Dispose();
        }
    }
}