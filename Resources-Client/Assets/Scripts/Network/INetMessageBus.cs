using System;
using Network.Protocol;

namespace Network
{
    public interface INetMessageBus
    {
        void AddMessageHandler<T>(Action<T> handler) where T : INetMessage;
        void RemoveMessageHandler<T>(Action<T> handler) where T : INetMessage;
    }
}