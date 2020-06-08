using Services.Network;
using Services.UIService;

namespace Services
{
    public interface IServices
    {
        IGameResourceService ResourceService { get; }
        INetService NetService { get; }
        IUiService UiService { get; }
    }
}