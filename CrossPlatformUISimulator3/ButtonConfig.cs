using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class ButtonConfig : IPrototypical<ButtonConfig>
    {
        public string Text { get; set; } = "";
        public string? IconPath { get; set; }

        public ButtonConfig Clone()
        {
            return new ButtonConfig
            {
                Text = this.Text,
                IconPath = this.IconPath
            };
        }
    }
}