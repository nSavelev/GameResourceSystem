using System.Collections.Generic;
using DataModel.GameResources;

namespace Network.Protocol
{
    public class ResourceAmountChangedMessage : INetMessage
    {
        public IEnumerable<(ResourceId, int)> ResourceAmounts;
    }
}