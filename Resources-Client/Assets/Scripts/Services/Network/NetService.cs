using Network;
using TempLogic;

namespace Services.Network
{
    public class NetService : INetService
    {
        public INetMessageBus NetMessageBus => _network;
        public INetSender NetSender => _network;
        private TempNetwork _network;

        public NetService()
        {
            _network = new TempNetwork();
        }

        public void Connect()
        {
            _network.Connect();
        }
    }
}