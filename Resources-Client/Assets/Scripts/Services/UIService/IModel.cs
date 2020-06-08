using System;

namespace Services.UIService
{
    public interface IModel : IDisposable
    {
        void Init(IServices services);
    }
}