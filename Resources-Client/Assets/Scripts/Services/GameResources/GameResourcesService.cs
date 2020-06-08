using DataModel.GameData;
using DataModel.GameResources;
using Network;
using Network.Protocol;
using UnityEngine;

namespace Services.GameResources
{
    public class GameResourcesService : IGameResourceService
    {
        public IResourceStorage ResourceStorage => _storage;
        public IClientResourceOperations ClientResourceOperations => _storage;

        private INetMessageBus _messageBus;
        private ClientResourcesStorage _storage;

        public GameResourcesService(INetMessageBus messageBus)
        {
            _messageBus = messageBus;
            _storage = new ClientResourcesStorage();
            _messageBus.AddMessageHandler<InitialDataMessage>(OnInitialData);
        }

        private void OnInitialData(InitialDataMessage msg)
        {
            _messageBus.RemoveMessageHandler<InitialDataMessage>(OnInitialData);
            foreach (var data in msg.ResourcesData)
            {
                _storage.SetAmount(data.Item1, data.Item2);
                _storage.SetLimit(data.Item1, data.Item3);
            }
            _messageBus.AddMessageHandler<ResourceAmountChangedMessage>(OnResourceAmountChanged);
            _messageBus.AddMessageHandler<ResourceLimitsChangedMessage>(OnResourceLimitsChanged);
        }

        private void OnResourceLimitsChanged(ResourceLimitsChangedMessage msg)
        {
            foreach (var limit in msg.ResourceLimits)
            {
                _storage.SetLimit(limit.Item1, limit.Item2);
            }
        }

        private void OnResourceAmountChanged(ResourceAmountChangedMessage msg)
        {
            foreach (var amount in msg.ResourceAmounts)
            {
                _storage.SetAmount(amount.Item1, amount.Item2);
            }
        }

        public void Dispose()
        {
            _messageBus.RemoveMessageHandler<InitialDataMessage>(OnInitialData);
            _messageBus.RemoveMessageHandler<ResourceAmountChangedMessage>(OnResourceAmountChanged);
            _messageBus.RemoveMessageHandler<ResourceLimitsChangedMessage>(OnResourceLimitsChanged);
        }
    }
}