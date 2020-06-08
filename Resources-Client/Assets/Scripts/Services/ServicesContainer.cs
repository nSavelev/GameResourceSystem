using Services.GameResources;
using Services.Network;
using Services.UIService;

namespace Services
{
    public class ServicesContainer : IServices
    {
        private NetService _network;
        private GameResourcesService _resources;
        private IUiService _uiService;
        public IGameResourceService ResourceService => _resources;
        public INetService NetService => _network;
        public IUiService UiService => _uiService;

        public ServicesContainer()
        {
            _network = new NetService();
            _resources = new GameResourcesService(NetService.NetMessageBus);
        }
        
        public void Init()
        {
            // TODO: add UI service
            _network.Connect();
        }

        public void AddUIService(IUiService uiService)
        {
            _uiService = uiService;
        }
    }
}