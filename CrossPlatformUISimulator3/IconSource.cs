using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class IconSource : IPrototypical<IconSource>
    {
        public string Path { get; set; } = "";

        public IconSource Clone()
        {
            return new IconSource { Path = this.Path };
        }
    }
}