using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace CrossPlatformUISimulator3
{
    public class LabelComponent : IUIComponent
    {
        public string Id { get; init; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Text { get; set; }
        public int FontSize { get; set; }

        public LabelComponent(string id, int x, int y, string text, int fontSize = 12)
        {
            Id = id;
            X = x;
            Y = y;
            Text = text;
            FontSize = fontSize;
        }

        public string Render() => RenderingManager.CurrentStrategy.DrawText(new Point(X, Y), Text, FontSize);

        public IUIComponent? FindById(string id) => Id == id ? this : null;

        public IUIComponent Clone() => new LabelComponent(Id, X, Y, Text, FontSize);
    }

    public class SliderComponent : IUIComponent
    {
        public string Id { get; init; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }

        public SliderComponent(string id, int x, int y, int value = 50)
        {
            Id = id;
            X = x;
            Y = y;
            Value = value;
        }

        public string Render()
        {
            var track = RenderingManager.CurrentStrategy.DrawRectangle(new Rectangle(X, Y, 150, 20), $"Slider track, val={Value}");
            var handle = RenderingManager.CurrentStrategy.DrawText(new Point(X + (Value * 15), Y), "||", 12);
            return $"{track}\n{handle}";
        }

        public IUIComponent? FindById(string id) => Id == id ? this : null;

        public IUIComponent Clone() => new SliderComponent(Id, X, Y, Value);
    }
}
