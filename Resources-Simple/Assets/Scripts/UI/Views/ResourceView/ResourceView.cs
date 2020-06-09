using System.Collections.Generic;
using Data;
using UI.Presenters;
using UI.UIData;
using UnityEngine;

namespace UI.Views.ResourceView
{
    public class ResourceView : MonoBehaviour, IResourceView
    {
        [SerializeField]
        private ResourceIconsMap _iconsMap = null;
        [SerializeField]
        private ResourceItemView _itemViewPrefab = null;
        [SerializeField]
        private RectTransform _container = null;

        private Dictionary<ResourceId, ResourceItemView> _views = new Dictionary<ResourceId, ResourceItemView>();

        public void InitView(IResourceViewPresenter presenter)
        {
            // TODO: for some ui actions
        }
        
        public void UpdateResourceValue(ResourceId id, int value)
        {
            if (!_views.TryGetValue(id, out var view))
            {
                view = _views[id] = Instantiate(_itemViewPrefab, _container);
                view.Init(GetResourceIcon(id), value);
            }
            else
            {
                view.SetValue(value);
            }
        }

        private Sprite GetResourceIcon(ResourceId id)
        {
            return _iconsMap.GetIcon(id);
        }
    }
}