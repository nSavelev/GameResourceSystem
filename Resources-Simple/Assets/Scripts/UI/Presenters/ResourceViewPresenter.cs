using System;
using System.Collections.Generic;
using Data;
using UI.Views.ResourceView;
using UniRx;

namespace UI.Presenters
{
    // TODO: ui input actions
    public interface IResourceViewPresenter
    {
    }
    
    public class ResourceViewPresenter : IResourceViewPresenter, IDisposable
    {
        private List<IDisposable> _disposables = new List<IDisposable>();
        private IResourceView _view;

        public ResourceViewPresenter(ResourcesData data)
        {
            data.Resources.ObserveAdd().Subscribe(change => { RaiseResourceAmountChange(change.Key, change.Value);}).AddTo(_disposables);
            data.Resources.ObserveReplace().Subscribe(change => { RaiseResourceAmountChange(change.Key, change.NewValue);}).AddTo(_disposables);
            data.Resources.ObserveRemove().Subscribe(change => { RaiseResourceAmountChange(change.Key, change.Value);}).AddTo(_disposables);
        }

        private void RaiseResourceAmountChange(ResourceId id, int value)
        {
            _view?.UpdateResourceValue(id, value);
        }

        public void SetView(IResourceView view)
        {
            _view = view;
            _view.InitView(this);
        }

        public void Dispose()
        {
            _view = null;
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}