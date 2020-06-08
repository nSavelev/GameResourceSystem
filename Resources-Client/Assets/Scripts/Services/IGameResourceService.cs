using DataModel.GameResources;

namespace Services
{
    public interface IGameResourceService
    {
        IResourceStorage ResourceStorage { get; }
        IClientResourceOperations ClientResourceOperations { get; }
    }
}