using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public interface IDialogRenderer
    {
        void Render(IDialog dialog);
    }
}