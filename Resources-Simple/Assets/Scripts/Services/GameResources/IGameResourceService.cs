using System.Collections.Generic;
using Data;
using UniRx;

namespace Services.GameResources
{
    public interface IGameResourceService
    {
        ResourcesData Data { get; }

        void SetResource(ResourceId id, int value);
        void ChangeResource(ResourceId id, int value);

        void SetResources(IEnumerable<(ResourceId, int)> values);

        void ChangeResources(IEnumerable<(ResourceId, int)> deltas);
    }
}