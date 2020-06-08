using System.Collections.Generic;
using DataModel.GameResources;

namespace Network.Protocol
{
    public class ResourceLimitsChangedMessage : INetMessage
    {
        public IEnumerable<(ResourceId, int?)> ResourceLimits;
    }
}