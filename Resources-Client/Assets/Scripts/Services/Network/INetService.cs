using Network;

namespace Services.Network
{
    public interface INetService
    {
        INetMessageBus NetMessageBus { get; }
    }
}