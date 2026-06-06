using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class UIContainerComponent : IUIComponent
    {
        private readonly List<IUIComponent> _children = new();

        public string Id { get; init; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Title { get; set; }
        public string ThemeName { get; set; }

        public IReadOnlyList<IUIComponent> Children => _children.AsReadOnly();

        public UIContainerComponent(string id, string title, string themeName)
        {
            Id = id;
            Title = title;
            ThemeName = themeName;
        }

        public void AddChild(IUIComponent component)
        {
            _children.Add(component);
        }

        public string Render()
        {
            var header = $"[Container ID={Id} Title='{Title}' Theme={ThemeName} at ({X},{Y})]";
            var childrenRenders = _children.Select(c => "  " + c.Render());
            return string.Join(Environment.NewLine, childrenRenders.Prepend(header));
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
            var clone = new UIContainerComponent(Id, Title, ThemeName)
            {
                X = this.X,
                Y = this.Y
            };
            foreach (var child in _children)
            {
                clone.AddChild(child.Clone());
            }
            return clone;
        }
    }
}
