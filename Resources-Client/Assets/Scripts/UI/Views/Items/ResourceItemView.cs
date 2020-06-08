using DataModel.GameResources;
using UI.UIData;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.Items
{
    public class ResourceItemView : MonoBehaviour
    {
        [SerializeField]
        private GameResourcesUiData _resourcesUiData;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Text _label;
        [SerializeField]
        private StupidProgressBar _fillRate;

        private int? _limit;
        private int _amount;

        public void Init(ResourceId id, IReadOnlyReactiveProperty<int> amount, IReadOnlyReactiveProperty<int?> limit)
        {
            _icon.sprite = _resourcesUiData.GetResourceIcon(id);
            amount.Subscribe(v => { 
                _amount = v;
                UpdateView();
            }).AddTo(this);
            limit.Subscribe(v =>
            {
                _limit = v;
                UpdateView();
            }).AddTo(this);
        }

        private void UpdateView()
        {
            var rate = 0f;
            if (_limit.HasValue)
            {
                rate = (float)_amount / _limit.Value;
                _fillRate.SetValue(rate);
                _label.text = $"{_amount}/{_limit.Value}";
            }
            else
            {
                _label.text = $"{_amount}";
            }
            _fillRate.SetVisible(_limit.HasValue);
        }
    }
}
