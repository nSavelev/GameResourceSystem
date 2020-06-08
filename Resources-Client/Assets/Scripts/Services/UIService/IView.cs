using System;

namespace Services.UIService
{
    public interface IView
    {
        Type ModelType { get; }

        void Show();
        void Hide();
    }
}