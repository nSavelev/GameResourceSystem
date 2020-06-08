using System.Collections.Generic;
using DataModel.GameResources;
using Services.UIService;
using UI.Models;
using UI.Views.Items;
using UniRx;
using UnityEngine;

namespace UI.Views
{
    public class ResourcesView : GenericAbstractView<ResourcesModel>
    {
        [SerializeField]
        private ResourceItemView _resourceView = null;
        [SerializeField]
        private RectTransform _resoucesViewsContainer = null;

        private Dictionary<ResourceId, ResourceItemView> _resourceItemViews = new Dictionary<ResourceId, ResourceItemView>();
        
        protected override void OnIniting(ResourcesModel model)
        {
            model.ResourceItems.ObserveAdd().Subscribe(newItem =>
            {
                var resouceItem = newItem.Value;
                if (!_resourceItemViews.ContainsKey(resouceItem.Id))
                {
                    var view = Instantiate(_resourceView, _resoucesViewsContainer);
                    view.Init(resouceItem.Id, resouceItem.Amount, resouceItem.Limit);
                }
            }).AddTo(this);

            model.ResourceItems.ObserveRemove().Subscribe(removeItem =>
            {
                var data = removeItem.Value;
                if (_resourceItemViews.TryGetValue(data.Id, out var view))
                {
                    Destroy(view.gameObject);
                }
            }).AddTo(this);
        }
    }
}