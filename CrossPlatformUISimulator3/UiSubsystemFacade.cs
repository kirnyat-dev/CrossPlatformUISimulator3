using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class UiSubsystemFacade
    {
        private readonly IWidgetFactory _widgetFactory;

        public UiSubsystemFacade(IWidgetFactory widgetFactory)
        {
            _widgetFactory = widgetFactory ?? throw new ArgumentNullException(nameof(widgetFactory));
        }

        public UIContainerComponent CreateStandardDialog(string id, string title, string defaultText)
        {
            var start = DateTime.Now;

            var container = new UIContainerComponent(id, title, "DefaultTheme");

            IUIComponent label = new TextLabelComponent(id + "_lbl", 10, 20, defaultText);

            label = new CachedRenderDecorator(
                        new LogRenderDecorator(
                            new BorderDecorator(label, "Dotted")
                        )
                    );

            container.AddChild(label);

            ApplicationTelemetrySingleton.Instance.LogOperation("Facade", "CreateDialog", DateTime.Now - start);
            return container;
        }

        public void ApplyGlobalTheme(UIContainerComponent root, string newThemeName)
        {
            root.ThemeName = newThemeName;

            ApplicationTelemetrySingleton.Instance.LogOperation("Facade", "ApplyTheme", TimeSpan.Zero);
        }

        public string RenderAllToContext(IUIComponent root)
        {
            var start = DateTime.Now;
            var output = root.Render();
            ApplicationTelemetrySingleton.Instance.LogOperation("Facade", "RenderAll", DateTime.Now - start);
            return output;
        }
    }
}