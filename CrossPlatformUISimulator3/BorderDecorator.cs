using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class BorderDecorator : UIComponentDecorator
    {
        public string BorderStyle { get; set; }

        public BorderDecorator(IUIComponent component, string borderStyle = "Solid") : base(component)
        {
            BorderStyle = borderStyle;
        }

        public override string Render()
        {
            return $"Border({BorderStyle}) -> {base.Render()}";
        }

        public override IUIComponent Clone()
        {
            return new BorderDecorator(component.Clone(), BorderStyle);
        }
    }
}
