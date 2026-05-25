using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class LegacyGraphicsEngine
    {
        public void InitializeRawContext(IntPtr windowHandle) { }
        public void DrawNativeButton(int x, int y, int width, int height, string legacyLabel)
            => Console.WriteLine($"[Legacy] Button: {legacyLabel} at {x},{y}");
        public void RenderTextRaster(string fontName, int size, int r, int g, int b, int x, int y, string text) { }
        public void ShowModalWindow(IntPtr parent, string title, bool blockInput) { }
    }
}