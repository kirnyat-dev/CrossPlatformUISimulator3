using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public interface IDialog : IPrototypical<IDialog>
    {
        string Title { get; }
        IThemeFactory Theme { get; }
        IReadOnlyList<IWidget> Buttons { get; }
        IconSource? Icon { get; }
        IReadOnlyList<IWidget> CustomWidgets { get; }
        void Show();
    }
}