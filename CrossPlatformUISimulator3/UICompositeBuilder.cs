using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace CrossPlatformUISimulator3
{
    public class UICompositeBuilder
    {
        private UICompositeContainer? _root;

        public UICompositeBuilder() => Reset();

        public void Reset() => _root = new UICompositeContainer("root_panel", 0, 0, "DefaultPanel");

        public UICompositeBuilder SetContainer(string id, int x, int y, string style)
        {
            _root = new UICompositeContainer(id, x, y, style);
            return this;
        }

        public UICompositeBuilder AddLabel(string id, int x, int y, string text)
        {
            _root?.AddChild(new LabelComponent(id, x, y, text));
            return this;
        }

        public UICompositeBuilder AddSlider(string id, int x, int y, int value)
        {
            _root?.AddChild(new SliderComponent(id, x, y, value));
            return this;
        }

        public UICompositeContainer Build()
        {
            var start = DateTime.Now;
            if (_root == null) throw new InvalidOperationException("Билдер не инициализирован");

            ApplicationTelemetrySingleton.Instance.LogOperation("Builder", "BuildTree", DateTime.Now - start);

            var result = _root;
            Reset();
            return result;
        }
    }
}
