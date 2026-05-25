using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class DialogBuilder : IContainerBuilder
    {
        private string? _title;
        private IThemeFactory? _theme;
        private readonly List<IWidget> _buttons = new();
        private IconSource? _icon;
        private readonly List<IWidget> _customWidgets = new();
        private IWidgetFactory _widgetFactory;

        public DialogBuilder(IWidgetFactory widgetFactory)
        {
            _widgetFactory = widgetFactory;
        }

        public IContainerBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public IContainerBuilder AddButton(ButtonConfig config)
        {
            _buttons.Add(_widgetFactory.CreateButton(config));
            return this;
        }

        public IContainerBuilder SetIcon(IconSource source)
        {
            _icon = source;
            return this;
        }

        public IContainerBuilder ConfigureTheme(IThemeFactory theme)
        {
            _theme = theme;
            return this;
        }

        public IContainerBuilder AddCustomWidget(IWidget widget)
        {
            _customWidgets.Add(widget);
            return this;
        }

        public IDialog Build()
        {
            var start = DateTime.Now;
            if (string.IsNullOrWhiteSpace(_title))
                throw new InvalidOperationException("Dialog must have a title.");
            if (_buttons.Count == 0)
                throw new InvalidOperationException("Dialog must have at least one button.");
            if (_theme == null)
                throw new InvalidOperationException("Theme must be configured.");
            var dialog = new Dialog(_title, _theme, _buttons.AsReadOnly(), _icon, _customWidgets.AsReadOnly());

            ApplicationTelemetrySingleton.Instance.LogOperation("Builder", "Build", DateTime.Now - start);
            return dialog;
        }
    }
}