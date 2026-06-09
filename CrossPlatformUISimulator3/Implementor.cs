using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public record Point(int X, int Y);
    public record Rectangle(int X, int Y, int Width, int Height);

    public interface IRenderingStrategy
    {
        string GetStrategyName();
        string DrawRectangle(Rectangle box, string styleInfo);
        string DrawText(Point position, string text, int size);
    }

    public class VectorRenderingStrategy : IRenderingStrategy
    {
        public string GetStrategyName() => "VectorEngine";
        public string DrawRectangle(Rectangle box, string styleInfo) =>
            $"[Vector] Draw Rect: X={box.X}, Y={box.Y}, W={box.Width}, H={box.Height} ({styleInfo})";
        public string DrawText(Point position, string text, int size) =>
            $"[Vector] Render Text '{text}' at ({position.X},{position.Y}) with size {size}";
    }

    public class LegacyEngineRenderingAdapter : IRenderingStrategy
    {
        private readonly LegacyGraphicsEngine _legacyEngine = new();

        public string GetStrategyName() => "LegacyEngineAdapter";
        public string DrawRectangle(Rectangle box, string styleInfo)
        {
            _legacyEngine.DrawNativeButton(box.X, box.Y, box.Width, box.Height, styleInfo);
            return $"[Adapter->Legacy] Container drawn at {box.X},{box.Y}";
        }
        public string DrawText(Point position, string text, int size)
        {
            _legacyEngine.RenderTextRaster("Arial", size, 0, 0, 0, position.X, position.Y, text);
            return $"[Adapter->Legacy] Text '{text}' rasterized";
        }
    }

    public static class RenderingManager
    {
        public static IRenderingStrategy CurrentStrategy { get; set; } = new VectorRenderingStrategy();
    }
}
