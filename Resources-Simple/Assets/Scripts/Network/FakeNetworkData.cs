using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Network
{
    [CreateAssetMenu(menuName = "Data/Fake/FakeResourceNetData")]
    public class FakeNetworkData : ScriptableObject, IFakeNetworkData
    {
        [Serializable]
        private struct ResourceData
        {
            public ResourceId Id;
            public int Amount;

            public static ResourceData GetRandom()
            {
                return new ResourceData()
                {
                    Id = (ResourceId)Random.Range(0, Enum.GetValues(typeof(ResourceId)).Length),
                    Amount = Random.Range(1, 1000)
                };
            }
        }


        [SerializeField]
        private List<ResourceData> _initialResources = new List<ResourceData>();
        
        public IEnumerable<(ResourceId, int)> GetInitialResources()
        {
            return _initialResources.Select(e => (e.Id, e.Amount));
        }

        public IEnumerable<(ResourceId, int)> GetRandomResources()
        {
            var size = Random.Range(1, 3);
            var result = new List<(ResourceId, int)>();
            for (int i = 0; i < size; i++)
            {
                var rnd = ResourceData.GetRandom();
                result.Add((rnd.Id, rnd.Amount));
            }
            return result;
        }
    }
}