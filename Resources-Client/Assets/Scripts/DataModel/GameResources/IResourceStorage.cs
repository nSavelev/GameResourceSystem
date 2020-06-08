using UniRx;

namespace DataModel.GameResources
{
    public interface IResourceStorage
    {
        /// <summary>
        /// Enumerable of all counting resources
        /// </summary>
        IReadOnlyReactiveCollection<ResourceId> ResourceTypes { get; }

        /// <summary>
        /// GetResource amount reactive property
        /// </summary>
        IReadOnlyReactiveProperty<int> GetResourceAmount(ResourceId id);

        /// <summary>
        /// GetResource amount reactive property. null - unlimited
        /// </summary>
        IReadOnlyReactiveProperty<int?> GetResourceLimit(ResourceId id);
        
    }
}