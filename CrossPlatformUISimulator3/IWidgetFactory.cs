using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public interface IWidgetFactory
    {
        IWidget CreateButton(ButtonConfig config);
        IWidget CreateCustom(string name);
        IDialogRenderer CreateLegacyRenderer();
    }
}