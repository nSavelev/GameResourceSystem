using Services;
using Services.UIService;
using UI.Models;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    public IServices Services => _services;

    private ServicesContainer _services = null;

    [SerializeField]
    private UiService _uiService = null; 

    void Start()
    {
        var services = new ServicesContainer();
        services.AddUIService(_uiService);
        _uiService.Init(services);
        services.Init();
        _services = services;
        FinishInit();
    }

    private void FinishInit()
    {
        _uiService.Show<ResourcesModel>();
    }
}
