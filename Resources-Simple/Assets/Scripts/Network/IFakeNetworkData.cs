using System.Collections.Generic;
using Data;

namespace Network
{
    public interface IFakeNetworkData
    {
        IEnumerable<(ResourceId, int)> GetInitialResources();
        IEnumerable<(ResourceId, int)> GetRandomResources();
    }
}