namespace Services.UIService
{
    public interface IUiService
    {
        void RegisterView<TView>(TView view) where TView : IView;
        
        void Show<TModel>() where TModel : IModel;
        void Hide<TModel>() where TModel : IModel;
    }
}