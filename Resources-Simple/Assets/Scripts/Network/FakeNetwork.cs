using System;
using System.Collections.Generic;
using Data;
using UniRx;
using Random = UnityEngine.Random;

namespace Network
{
    public class FakeNetwork : INetwork
    {
        private IFakeNetworkData _fakeData;

        #region Sender

        public void SendResourcesDeltas(IEnumerable<(ResourceId, int)> deltas)
        {
            throw new NotImplementedException();
        }

        public void SendResourceValues(IEnumerable<(ResourceId, int)> values)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Receiver

        public event Action<IEnumerable<(ResourceId, int)>> ResourcesChanged;

        #endregion

        public FakeNetwork(IFakeNetworkData fakeData)
        {
            _fakeData = fakeData;
        }

        public void Connect()
        {
            ResourcesChanged?.Invoke(_fakeData.GetInitialResources());
            PlanFakeResourceChange();
            
        }

        private void PlanFakeResourceChange()
        {
            Scheduler.MainThread.Schedule(TimeSpan.FromMilliseconds(Random.Range(200, 4000)), () =>
                {
                    ResourcesChanged?.Invoke(_fakeData.GetRandomResources());
                    PlanFakeResourceChange();
                });
        }

        public void Disconnect()
        {
        }
    }
}