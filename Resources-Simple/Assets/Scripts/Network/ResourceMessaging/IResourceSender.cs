using System.Collections.Generic;
using Data;

namespace Network.ResourceMessaging
{
    public interface IResourceSender
    {
        void SendResourcesDeltas(IEnumerable<(ResourceId, int)> deltas);
        void SendResourceValues(IEnumerable<(ResourceId, int)> values);
    }
}