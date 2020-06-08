using System;
using System.Collections.Generic;
using DataModel.GameResources;
using UniRx;
using UnityEngine;

namespace DataModel.GameData
{
    /////////////////////////////////////////////////////////////////////////////////
    // Amount and Limit containers are separated to prevent meaningless change events
    /////////////////////////////////////////////////////////////////////////////////

    public class ClientResourcesStorage : IResourceStorage, IClientResourceOperations, IServerResourceOperations
    {
        #region IResourceStorage

        public IReadOnlyReactiveCollection<ResourceId> ResourceTypes => _resourceIds;
        
        private ReactiveCollection<ResourceId> _resourceIds = new ReactiveCollection<ResourceId>();
        private Dictionary<ResourceId, ReactiveProperty<int>> _resourceAmounts = new Dictionary<ResourceId, ReactiveProperty<int>>();
        private Dictionary<ResourceId, ReactiveProperty<int?>> _resourceLimits = new Dictionary<ResourceId, ReactiveProperty<int?>>();

        
        public IReadOnlyReactiveProperty<int> GetResourceAmount(ResourceId id)
        {
            if (_resourceAmounts.TryGetValue(id, out var amount))
            {
                return amount;
            }
            throw new ArgumentException($"Try getting amount of unregistered resource {id}");
        }

        // TODO: move to IResourceData abstraction if need more operations like this, no needed for just 2
        // TODO: maybe throw an exception if no limit found?
        public IReadOnlyReactiveProperty<int?> GetResourceLimit(ResourceId id)
        {
            // resources are unlimited by default
            if (!_resourceLimits.TryGetValue(id, out var limit))
            {
                _resourceLimits[id] = limit = new ReactiveProperty<int?>(null);
            }
            return limit;
        }

        #endregion

        #region IResourceOperations

        public void Receive(ResourceId id, int amount)
        {
            if (amount < 0)
            {
                throw new Exception("Trying to receive negative amount");
            }
            if (!_resourceAmounts.TryGetValue(id, out var currentAmount))
            {
                 _resourceAmounts[id] = currentAmount = new ReactiveProperty<int>();
            }

            // Clamp resource limit
            if (_resourceLimits.TryGetValue(id, out var limit))
            {
                var limitValue = limit.Value;
                if (limitValue.HasValue)
                {
                    currentAmount.Value = Mathf.Clamp(currentAmount.Value + amount, 0, limitValue.Value);
                    return;
                }
            }

            currentAmount.Value += amount;
        }

        public bool TrySpend(ResourceId id, int amount)
        {
            if (amount < 0)
            {
                throw new Exception("Trying to spend negative amount");
            }
            if (_resourceAmounts.TryGetValue(id, out var currentAmount))
            {
                if (currentAmount.Value >= amount)
                {
                    currentAmount.Value -= amount;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region ServerResourceOperations

        public void SetAmount(ResourceId id, int amount)
        {
            if (!_resourceAmounts.TryGetValue(id, out var currentAmount))
            {
                _resourceAmounts[id] = currentAmount = new ReactiveProperty<int>();
                _resourceIds.Add(id);
            }

            if (_resourceLimits.TryGetValue(id, out var limit))
            {
                var limitValue = limit.Value;
                if (limitValue.HasValue)
                {
                    amount = Mathf.Clamp(amount, 0, limitValue.Value);
                }
            }
            currentAmount.Value = amount;
        }

        public void SetLimit(ResourceId id, int? limit)
        {
            if (!_resourceLimits.TryGetValue(id, out var currentLimit))
            {
                _resourceLimits[id] = currentLimit = new ReactiveProperty<int?>();
            }
            if (!_resourceAmounts.TryGetValue(id, out var amount))
            {
                _resourceAmounts.Add(id, amount = new ReactiveProperty<int>(0));
            }
            // Clamp resource count if limit decreased (or if setted from unlimited)
            if (amount.Value > limit || currentLimit.Value > limit || (!currentLimit.Value.HasValue && limit.HasValue))
            {
                amount.Value = Mathf.Clamp(amount.Value, 0, limit.Value);
            }
            currentLimit.Value = limit;
        }

        #endregion
    }
}