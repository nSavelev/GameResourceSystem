using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.Items
{
    public class StupidProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _progress = null;

        public void SetValue(float value)
        {
            _progress.fillAmount = value;
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}