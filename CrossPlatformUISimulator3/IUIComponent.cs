using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public interface IUIComponent : IPrototypical<IUIComponent>
    {
        string Id { get; }
        int X { get; set; }
        int Y { get; set; }
        string Render();
        IUIComponent? FindById(string id);
    }
}
