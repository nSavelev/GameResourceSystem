namespace DataModel.GameResources
{
    public interface IServerResourceOperations
    {
        /// <summary>
        /// Override resource amount
        /// </summary>
        void SetAmount(ResourceId id, int amount);

        /// <summary>
        /// Override resource limit. null - unlimited
        /// </summary>
        void SetLimit(ResourceId id, int? limit);
    }
}