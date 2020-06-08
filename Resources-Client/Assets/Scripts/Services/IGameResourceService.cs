using System;
using DataModel.GameResources;

namespace Services
{
    public interface IGameResourceService : IDisposable
    {
        IResourceStorage ResourceStorage { get; }
        IClientResourceOperations ClientResourceOperations { get; }
    }
}