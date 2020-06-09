using Network.ResourceMessaging;

namespace Network
{
    public interface INetworkReceiver : IResourceReceiver
    {
    }
    
    public interface INetworkSender : IResourceSender
    {
    }
    
    public interface INetwork : INetworkSender, INetworkReceiver
    {
        void Connect();
        void Disconnect();
    }
}