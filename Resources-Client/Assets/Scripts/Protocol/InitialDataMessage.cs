using System.Collections.Generic;
using DataModel.GameResources;

namespace Network.Protocol
{
    public class InitialDataMessage : INetMessage
    {
        public IEnumerable<(ResourceId, int, int?)> ResourcesData { get; set; }
    }
}