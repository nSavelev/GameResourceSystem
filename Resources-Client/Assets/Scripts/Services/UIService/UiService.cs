using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.UIService
{
    public class UiService : MonoBehaviour, IUiService
    {
        [SerializeField]
        private List<BaseAbstractView> _views = new List<BaseAbstractView>();

        private List<IModel> _models = new List<IModel>();
        private Dictionary<Type, IView> _viewsMap = new Dictionary<Type, IView>();
        
        public void Init(IServices services)
        {
            foreach (var view in _views)
            {
                var model = (IModel)Activator.CreateInstance(view.ModelType);
                _models.Add(model);
                model.Init(services);
                view.Init(model);
                RegisterView(view);
            }
        }

        public void RegisterView<TView>(TView view) where TView : IView
        {
            
            _viewsMap.Add(view.ModelType, view);
        }

        public void Show<TModel>() where TModel : IModel
        {
            if (_viewsMap.TryGetValue(typeof(TModel), out var view))
            {
                view.Show();
            }
            else
            {
                throw new ArgumentException($"No registered view for model type {typeof(TModel)}");
            }
        }

        public void Hide<TModel>() where TModel : IModel
        {
            if (_viewsMap.TryGetValue(typeof(TModel), out var view))
            {
                view.Hide();
            }
            else
            {
                throw new ArgumentException($"No registered view model type {typeof(TModel)}");
            }
        }
    }
}