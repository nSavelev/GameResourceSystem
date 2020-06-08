using System;
using System.Collections.Generic;
using System.Linq;
using Network;
using Network.Protocol;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TempLogic
{
    // For temp, no multi threading solution
    public class TempNetwork : INetMessageBus, INetSender, INetwork
    {
        private const string TEMP_DATA_PATH = "TempData/GameResources";
        
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
        private TempGameResourcesData _data;

        public void AddMessageHandler<T>(Action<T> handler) where T : INetMessage
        {
            var type = typeof(T);
            var wrap = new HandlerWrap(handler.GetHashCode(), (msg) =>
            {
                handler?.Invoke((T)msg);
            });
            if (!_handlers.TryGetValue(type, out var handlerWraps))
            {
                handlerWraps = new HashSet<HandlerWrap>();
                _handlers.Add(type, handlerWraps);
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
            if (_handlers.TryGetValue(message.GetType(), out var handlerWraps))
            {
                // some allocations to prevent collection change
                foreach (var wrap in handlerWraps.ToArray())
                {
                    wrap.Invoke(message);
                }
            }
        }

        public void Send(INetMessage message)
        {
            // Loopback for now
            Raise(message);
        }

        public void Connect()
        {
            _data = Resources.Load<TempGameResourcesData>(TEMP_DATA_PATH);
            Send(_data.GetStartResourcesInfoMessage());
            PlanFakeMessage();
        }

        private void PlanFakeMessage()
        {
            var waitMilliseconds = Random.Range(1, 10);
            var scheduleTimeSpan = new TimeSpan(0, 0, 0, waitMilliseconds);
            Scheduler.MainThread.Schedule(scheduleTimeSpan, () =>
            {
                Send(_data.GetRandomMessage());
                PlanFakeMessage();
            });
        }
        
        public void Disconnect()
        {
            
        }
    }
}
