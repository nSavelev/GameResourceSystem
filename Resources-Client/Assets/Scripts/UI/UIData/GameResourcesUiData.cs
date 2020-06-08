using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.GameResources;
using UnityEngine;

namespace UI.UIData
{
    [CreateAssetMenu(menuName = "Data/UI/GameRrsources")]
    public class GameResourcesUiData : ScriptableObject
    {
        #pragma warning disable 0649
        [Serializable]
        private class ResourceIconData
        {
            public string ResourceId;
            public Sprite Icon;
        }
        #pragma warning restore 0649

        [Header("GameResources")]
        [SerializeField]
        private Sprite _defaultResourceIcon = null;
        [SerializeField]
        private List<ResourceIconData> _resourceIcons = new List<ResourceIconData>();

        // TODO: Can be cached in Dictionary in case of perfomance loss
        public Sprite GetResourceIcon(ResourceId resourceId)
        {
            var data = _resourceIcons.FirstOrDefault(e => e.ResourceId == resourceId.Id);
            if (data == null)
            {
                return _defaultResourceIcon;
            }
            return data.Icon;
        }
    }
}