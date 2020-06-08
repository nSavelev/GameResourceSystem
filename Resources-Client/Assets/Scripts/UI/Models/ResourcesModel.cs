using System;
using System.Collections.Generic;
using DataModel.GameResources;
using Services;
using Services.UIService;
using UniRx;

namespace UI.Models
{
    public readonly struct ResourceItemData
    {
        public readonly ResourceId Id;
        public readonly IReadOnlyReactiveProperty<int> Amount;
        public readonly IReadOnlyReactiveProperty<int?> Limit;

        public ResourceItemData(ResourceId id, IReadOnlyReactiveProperty<int> amount, IReadOnlyReactiveProperty<int?> limit)
        {
            Id = id;
            Amount = amount;
            Limit = limit;
        }
    }
    
    public class ResourcesModel : IModel
    {
        public IReactiveCollection<ResourceItemData> ResourceItems => _resourceItems;
        private ReactiveCollection<ResourceItemData> _resourceItems = new ReactiveCollection<ResourceItemData>();
        
        private IResourceStorage _storage;

        private List<IDisposable> _disposables = new List<IDisposable>();

        // add here some input dependend logic if needed
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        public void Init(IServices services)
        {
            _storage = services.ResourceService.ResourceStorage;
            _storage.ResourceTypes.ObserveAdd().Subscribe(newItem =>
            {
                var resourceId = newItem.Value;
                var amount = _storage.GetResourceAmount(resourceId);
                var limit = _storage.GetResourceLimit(resourceId);
                _resourceItems.Add(new ResourceItemData(resourceId, amount, limit));
            }).AddTo(_disposables);
        }
    }
}