namespace Services
{
    public interface IServices
    {
        IGameResourceService ResourceService { get; }
        INetService NetService { get; }
    }
}