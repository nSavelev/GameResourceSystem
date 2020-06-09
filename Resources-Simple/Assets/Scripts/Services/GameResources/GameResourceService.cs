using System;
using System.Collections.Generic;
using Data;
using Network.ResourceMessaging;

namespace Services.GameResources
{
    public class GameResourceService : IGameResourceService, IDisposable
    {
        public ResourcesData Data => _data;
        private ResourcesData _data;
        private IResourceReceiver _reciever;
        private IResourceSender _sender;

        public GameResourceService()
        {
            _data = new ResourcesData();
        }

        public void Init(IResourceSender sender, IResourceReceiver receiver)
        {
            _reciever = receiver;
            _sender = sender;
            _reciever.ResourcesChanged += RecieverOnResourcesChanged;
        }

        private void RecieverOnResourcesChanged(IEnumerable<(ResourceId, int)> resources)
        {
            foreach (var resource in resources)
            {
                _data.SetResourceValue(resource.Item1, resource.Item2);
            }
        }

        public void SetResource(ResourceId id, int value)
        {
            SetResources(new (ResourceId, int)[]{(id, value)});
        }

        public void ChangeResource(ResourceId id, int value)
        {
            ChangeResources(new (ResourceId, int)[]{(id, value)});
        }

        public void SetResources(IEnumerable<(ResourceId, int)> values)
        {
            _sender.SendResourceValues(values);
        }

        public void ChangeResources(IEnumerable<(ResourceId, int)> deltas)
        {
            _sender.SendResourcesDeltas(deltas);
        }

        public void Dispose()
        {
            _reciever.ResourcesChanged -= RecieverOnResourcesChanged;
        }
    }
}