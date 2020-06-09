using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace UI.UIData
{
    [CreateAssetMenu(menuName = "Data/UI/ResourceIcons")]
    public class ResourceIconsMap : ScriptableObject
    {
        [Serializable]
        private struct ResourceIconInfo
        {
            public ResourceId Id;
            public Sprite Icon;
        }
        
        [SerializeField]
        private List<ResourceIconInfo> _icons = new List<ResourceIconInfo>();
        public Sprite GetIcon(ResourceId id)
        {
            return _icons.FirstOrDefault(e => e.Id == id).Icon;
        }
    }
}