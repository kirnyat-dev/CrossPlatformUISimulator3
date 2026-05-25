using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class ButtonWidget : IWidget
    {
        public ButtonConfig Config { get; private set; }

        public ButtonWidget(ButtonConfig config)
        {
            Config = config.Clone();
        }

        public IWidget Clone()
        {
            return new ButtonWidget(this.Config);
        }

        public string Render()
        {
            return $"Button: {Config.Text}";
        }
    }
}