using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class LogRenderDecorator : UIComponentDecorator
    {
        public LogRenderDecorator(IUIComponent component) : base(component) { }

        public override string Render()
        {
            var telemetry = ApplicationTelemetrySingleton.Instance;
            var start = DateTime.Now;

            var result = base.Render();

            telemetry.LogOperation("Decorator", "LogRender", DateTime.Now - start);
            return result;
        }

        public override IUIComponent Clone()
        {
            return new LogRenderDecorator(component.Clone());
        }
    }
}
