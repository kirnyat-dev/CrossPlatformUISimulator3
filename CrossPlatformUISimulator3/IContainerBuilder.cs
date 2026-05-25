using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public interface IContainerBuilder
    {
        IContainerBuilder SetTitle(string title);
        IContainerBuilder AddButton(ButtonConfig config);
        IContainerBuilder SetIcon(IconSource source);
        IContainerBuilder ConfigureTheme(IThemeFactory theme);
        IContainerBuilder AddCustomWidget(IWidget widget);
        IDialog Build();
    }
}