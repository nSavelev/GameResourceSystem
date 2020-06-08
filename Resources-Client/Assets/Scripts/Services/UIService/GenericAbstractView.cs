using System;

namespace Services.UIService
{
    public abstract class GenericAbstractView<TModel>:BaseAbstractView where TModel : IModel
    {
        public override Type ModelType => typeof(TModel);

        private TModel _model;

        internal override void Init(IModel model)
        {
            if (model.GetType() != ModelType)
                throw new ArgumentException($"Unexcpected mode type {model.GetType().FullName}. Expecting {ModelType.FullName}. Check initialization!");
            Init((TModel)model);
        }
        
        public void Init(TModel model)
        {
            _model = model;
            OnIniting(_model);
        }

        protected abstract void OnIniting(TModel model);

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}