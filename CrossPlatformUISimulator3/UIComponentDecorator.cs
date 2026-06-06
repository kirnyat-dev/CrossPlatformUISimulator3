using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public abstract class UIComponentDecorator : IUIComponent
    {
        protected readonly IUIComponent component;

        protected UIComponentDecorator(IUIComponent component)
        {
            this.component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public virtual string Id => component.Id;

        public virtual int X
        {
            get => component.X;
            set => component.X = value;
        }

        public virtual int Y
        {
            get => component.Y;
            set => component.Y = value;
        }

        public virtual string Render() => component.Render();

        public virtual IUIComponent? FindById(string id)
        {
            if (Id == id) return this;
            return component.FindById(id);
        }

        public abstract IUIComponent Clone();
    }
}