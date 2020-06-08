using Network.Protocol;

namespace Network
{
    public interface INetSender
    {
        void Send(INetMessage message);
    }
}