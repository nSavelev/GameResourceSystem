namespace DataModel.GameResources
{
    public interface IClientResourceOperations
    {
        /// <summary>
        /// Add resources if possible. if more then capacity, then add to maximum value
        /// </summary>
        void Receive(ResourceId id, int amount);

        /// <summary>
        /// Spend resources if possible. if not - return false
        /// </summary>
        bool TrySpend(ResourceId id, int amount);
    }
}