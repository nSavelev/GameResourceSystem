using System;
using Network;
using Services.GameResources;
using UI;
using UnityEngine;

namespace Services
{
    public class Services : MonoBehaviour
    {
        [SerializeField]
        private FakeNetworkData _fakeNetworkData = null;
        [SerializeField]
        private UiManager _uiManager = null;
        private GameResourceService _gameResources = null;
        private INetwork _network = null;

        void Awake()
        {
            _gameResources = new GameResourceService();
            _uiManager.Init(_gameResources);

            _network = new FakeNetwork(_fakeNetworkData);

            _gameResources.Init(_network, _network);

            _network.Connect();
        }

        private void OnDestroy()
        {
            _network.Disconnect();
            _gameResources.Dispose();
        }
    }
}