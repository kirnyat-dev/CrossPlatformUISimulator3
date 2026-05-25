using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class CustomWidget : IWidget
    {
        public string Name { get; private set; }

        public CustomWidget(string name)
        {
            Name = name;
        }

        public IWidget Clone()
        {
            return new CustomWidget(this.Name);
        }

        public string Render()
        {
            return $"CustomWidget: {Name}";
        }
    }
}