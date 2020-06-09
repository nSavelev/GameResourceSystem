using System;
using UniRx;

namespace Data
{
    public class ResourcesData
    {
        public IReadOnlyReactiveDictionary<ResourceId, int> Resources => _resources; 
        private ReactiveDictionary<ResourceId, int> _resources = new ReactiveDictionary<ResourceId, int>();

        public void SetResourceValue(ResourceId id, int value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"Resource {id} value {value} cant be less then zero");
            }
            _resources[id] = value;
        }

        public bool TrySpend(ResourceId id, int value)
        {
            if (_resources.TryGetValue(id, out var amount))
            {
                if (amount > value)
                {
                    _resources[id] = amount - value;
                }
            }
            return false;
        }

        public void Add(ResourceId id, int value)
        {
            if (!_resources.TryGetValue(id, out var amount))
            {
                _resources[id] = value;
            }
            else
            {
                _resources[id] = amount + value;
            }
        }
    }
}
