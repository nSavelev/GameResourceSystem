using System;
using System.Collections.Generic;
using Data;

namespace Network.ResourceMessaging
{
    public interface IResourceReceiver
    {
        event Action<IEnumerable<(ResourceId, int)>> ResourcesChanged;
    }
}