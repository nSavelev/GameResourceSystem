using System;
using System.Collections.Generic;
using System.Linq;
using Network;
using Network.Protocol;

namespace TempLogic
{
    // For temp, no multithreading solution
    public class TempNetwork : INetMessageBus
    {
        private class HandlerWrap
        {
            public readonly int Hash;
            private Action<INetMessage> _callback;

            public HandlerWrap(int actionHash, Action<INetMessage> callback)
            {
                Hash = actionHash;
                _callback = callback;
            }
            
            public void Invoke(INetMessage msg)
            {
                _callback?.Invoke(msg);
            }

            public override int GetHashCode()
            {
                return Hash;
            }
        }

        private Dictionary<Type, HashSet<HandlerWrap>> _handlers = new Dictionary<Type, HashSet<HandlerWrap>>();
        
        public void AddMessageHandler<T>(Action<T> handler) where T : INetMessage
        {
            var wrap = new HandlerWrap(handler.GetHashCode(), (msg) =>
            {
                handler?.Invoke((T)msg);
            });
            if (!_handlers.TryGetValue(typeof(T), out var handlerWraps))
            {
                handlerWraps = new HashSet<HandlerWrap>();
            }
            if (!handlerWraps.Contains(wrap))
            {
                handlerWraps.Add(wrap);
            }
            else
            {
                throw new ArgumentException("This handler already registered");
            }
        }

        public void RemoveMessageHandler<T>(Action<T> handler) where T : INetMessage
        {
            if (_handlers.TryGetValue(typeof(T), out var handlerWraps))
            {
                handlerWraps.RemoveWhere(e => e.Hash == handler.GetHashCode());
            }
        }

        public void Raise<T>(T message) where T : INetMessage
        {
            if (_handlers.TryGetValue(typeof(T), out var handlerWraps))
            {
                // some allocations to prevent collection change
                foreach (var wrap in handlerWraps.ToArray())
                {
                    wrap.Invoke(message);
                }
            }
        }
    }
}
