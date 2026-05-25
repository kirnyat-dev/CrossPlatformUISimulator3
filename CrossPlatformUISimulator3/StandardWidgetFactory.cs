using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class StandardWidgetFactory : IWidgetFactory
    {
        public static int CreationCount = 0;

        public IWidget CreateButton(ButtonConfig config)
        {
            Interlocked.Increment(ref CreationCount);
            return new ButtonWidget(config);
        }

        public IWidget CreateCustom(string name)
        {
            return new CustomWidget(name);
        }

        public IDialogRenderer CreateLegacyRenderer()
        {
            return new LegacyGraphicsAdapter();
        }
    }
}
