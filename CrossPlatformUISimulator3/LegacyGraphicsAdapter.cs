using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class LegacyGraphicsAdapter : IDialogRenderer
    {
        private readonly LegacyGraphicsEngine _engine = new();

        public void Render(IDialog dialog)
        {
            _engine.DrawNativeButton(0, 0, 100, 50, dialog.Title);
        }

        public void UnsupportedMethod()
        {
            throw new NotSupportedException("Данная операция не поддерживается устаревшим графическим движком.");
        }
    }
}