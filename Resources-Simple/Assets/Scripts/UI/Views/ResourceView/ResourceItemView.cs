using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Views.ResourceView
{
    public class ResourceItemView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon = null;
        [SerializeField]
        private Text _label = null;

        public void Init(Sprite icon, int value)
        {
            _icon.sprite = icon;
            SetValue(value);
        }

        public void SetValue(int value)
        {
            _label.text = ValueFormatter.FormatResourceValue(value);
        }
    }
}