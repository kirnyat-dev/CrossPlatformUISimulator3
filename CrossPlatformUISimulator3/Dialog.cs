using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class Dialog : IDialog
    {
        public string Title { get; init; }
        public IThemeFactory Theme { get; init; }
        public IReadOnlyList<IWidget> Buttons { get; init; }
        public IconSource? Icon { get; init; }
        public IReadOnlyList<IWidget> CustomWidgets { get; init; }

        internal Dialog(string title, IThemeFactory theme, IReadOnlyList<IWidget> buttons, IconSource? icon, IReadOnlyList<IWidget> customWidgets)
        {
            Title = title;
            Theme = theme;
            Buttons = buttons;
            Icon = icon?.Clone();
            CustomWidgets = customWidgets.Select(w => w.Clone()).ToList().AsReadOnly();
        }

        public IDialog Clone()
        {
            return new Dialog(
                this.Title,
                this.Theme,
                this.Buttons.Select(b => b.Clone()).ToList().AsReadOnly(),
                this.Icon,
                this.CustomWidgets.Select(c => c.Clone()).ToList().AsReadOnly()
            );
        }

        public void Show()
        {
            Console.WriteLine($"Dialog: {Title} (Theme: {Theme.GetThemeName()})");
            if (Icon != null) Console.WriteLine($"Icon: {Icon.Path}");
            foreach (var btn in Buttons) Console.WriteLine($"  {btn.Render()}");
            foreach (var cust in CustomWidgets) Console.WriteLine($"  {cust.Render()}");
        }
    }
}