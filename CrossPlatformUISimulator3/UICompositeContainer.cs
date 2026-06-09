using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace CrossPlatformUISimulator3
{
    public class UICompositeContainer : IUIComponent
    {
        private int _x;
        private int _y;
        private readonly List<IUIComponent> _children = new();

        public string Id { get; init; }
        public string Style { get; set; }
        public IReadOnlyList<IUIComponent> Children => _children.AsReadOnly();

        public int X
        {
            get => _x;
            set
            {
                int dx = value - _x;
                _x = value;
                foreach (var child in _children) child.X += dx;
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                int dy = value - _y;
                _y = value;
                foreach (var child in _children) child.Y += dy;
            }
        }

        public UICompositeContainer(string id, int x, int y, string style = "PanelStyle")
        {
            Id = id;
            _x = x;
            _y = y;
            Style = style;
        }

        public void AddChild(IUIComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            if (component == this || component.FindById(Id) != null)
            {
                throw new InvalidOperationException("Обнаружена попытка добавления циклической ссылки в дерево Composite.");
            }
            _children.Add(component);
        }

        public void RemoveChild(IUIComponent component) => _children.Remove(component);

        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine(RenderingManager.CurrentStrategy.DrawRectangle(new Rectangle(X, Y, 300, 300), $"Container: {Style}"));
            foreach (var child in _children)
            {
                sb.AppendLine(child.Render());
            }
            return sb.ToString().TrimEnd();
        }

        public IUIComponent? FindById(string id)
        {
            if (Id == id) return this;
            foreach (var child in _children)
            {
                var found = child.FindById(id);
                if (found != null) return found;
            }
            return null;
        }

        public IUIComponent Clone()
        {
            var clone = new UICompositeContainer(Id, X, Y, Style);
            foreach (var child in _children)
            {
                clone.AddChild(child.Clone());
            }
            return clone;
        }
    }
}
