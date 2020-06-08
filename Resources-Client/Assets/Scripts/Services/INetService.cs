using Network;

namespace Services
{
    public interface INetService
    {
        INetMessageBus NetMessageBus { get; }
    }
}