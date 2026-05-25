using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class MultiChoiceDialogDirector
    {
        public IDialog Construct(IContainerBuilder builder)
        {
            builder.SetTitle("Choose an option")
                   .ConfigureTheme(new DefaultThemeFactory())
                   .AddButton(new ButtonConfig { Text = "Yes" })
                   .AddButton(new ButtonConfig { Text = "No" })
                   .AddButton(new ButtonConfig { Text = "Cancel" })
                   .AddButton(new ButtonConfig { Text = "Help" })
                   .AddButton(new ButtonConfig { Text = "Skip" });
            return builder.Build();
        }
    }
}