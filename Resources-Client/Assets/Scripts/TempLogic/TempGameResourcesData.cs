using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.GameResources;
using Network.Protocol;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TempLogic
{
    [CreateAssetMenu(menuName = "Data/Temp/GameResources")]
    public class TempGameResourcesData : ScriptableObject
    {
        #pragma warning disable 0649
        [Serializable]
        private struct ResourceInfo
        {
            public string Id;
            public bool IsStartResource; 
            public int StartAmount;
            public bool IsStartLimit;
            public int StartLimit;
        }
        #pragma warning restore 0649
        
        [SerializeField]
        private List<ResourceInfo> _resourceInfos = new List<ResourceInfo>();

        // Net messages generation
        public InitialDataMessage GetStartResourcesInfoMessage()
        {
            var msg = new InitialDataMessage();
            // Linq cant handle with int? in tuple... I don't want to research 
            var startResoucesData = new List<(ResourceId, int, int?)>();
            foreach (var info in _resourceInfos)
            {
                if (info.IsStartResource)
                {
                    int? limit = null;
                    if (info.IsStartLimit)
                    {
                        limit = info.StartLimit;
                    }
                    startResoucesData.Add((new ResourceId(info.Id), info.StartAmount, limit));
                }
            }

            msg.ResourcesData = startResoucesData;
            return msg;
        }

        private ResourceAmountChangedMessage GetRandomAmountsChange()
        {
            var msg = new ResourceAmountChangedMessage(); 
            var amounts = new List<(ResourceId, int)>();
            foreach (var info in _resourceInfos)
            {
                if (Random.value > 0.5f)
                {
                    amounts.Add((new ResourceId(info.Id), Random.Range(0, 1000)));
                }
            }
            msg.ResourceAmounts = amounts;
            return msg;
        }

        private ResourceLimitsChangedMessage GetRandomLimitsChange()
        {
            var msg = new ResourceLimitsChangedMessage(); 
            var limits = new List<(ResourceId, int?)>();
            foreach (var info in _resourceInfos)
            {
                if (Random.value > 0.75f)
                {
                    if (Random.value < 0.1f)
                        limits.Add((new ResourceId(info.Id), null));
                    else
                        limits.Add((new ResourceId(info.Id), Random.Range(10, 1000)));
                }
            }
            msg.ResourceLimits = limits;
            return msg;
        }

        public INetMessage GetRandomMessage()
        {
            if (Random.value > 0.7f)
            {
                return GetRandomLimitsChange();
            }
            else
            {
                return GetRandomAmountsChange();
            }
        }
    }
}