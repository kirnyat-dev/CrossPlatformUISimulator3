using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class ErrorDialogDirector
    {
        public IDialog Construct(IContainerBuilder builder)
        {
            return builder.SetTitle("Error")
                          .SetIcon(new IconSource { Path = "error.png" })
                          .AddButton(new ButtonConfig { Text = "OK" })
                          .ConfigureTheme(new ErrorThemeFactory())
                          .Build();
        }
    }
}