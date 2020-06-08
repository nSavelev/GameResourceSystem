using System;
using UnityEngine;

namespace Services.UIService
{
    public abstract class BaseAbstractView : MonoBehaviour, IView
    {
        public abstract Type ModelType { get; }

        internal abstract void Init(IModel model);

        public abstract void Show();

        public abstract void Hide();
    }
}