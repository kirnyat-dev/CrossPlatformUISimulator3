using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class CachedRenderDecorator : UIComponentDecorator
    {
        private string? _cachedOutput;
        private int _lastX;
        private int _lastY;

        public CachedRenderDecorator(IUIComponent component) : base(component)
        {
            _lastX = component.X;
            _lastY = component.Y;
        }

        public override string Render()
        {
            if (component.X != _lastX || component.Y != _lastY)
            {
                _cachedOutput = null;
                _lastX = component.X;
                _lastY = component.Y;
            }

            if (_cachedOutput == null)
            {
                _cachedOutput = base.Render();
            }
            else
            {
                ApplicationTelemetrySingleton.Instance.LogOperation("Decorator", "CacheHit", System.TimeSpan.Zero);
            }

            return _cachedOutput;
        }

        public override IUIComponent Clone()
        {
            return new CachedRenderDecorator(component.Clone());
        }
    }
}