using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class TextLabelComponent : IUIComponent
    {
        public string Id { get; init; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Text { get; set; }

        public TextLabelComponent(string id, int x, int y, string text)
        {
            Id = id;
            X = x;
            Y = y;
            Text = text;
        }

        public string Render()
        {
            return $"[Label ID={Id} at ({X},{Y}): '{Text}']";
        }

        public IUIComponent? FindById(string id)
        {
            return Id == id ? this : null;
        }

        public IUIComponent Clone()
        {
            return new TextLabelComponent(Id, X, Y, Text);
        }
    }
}