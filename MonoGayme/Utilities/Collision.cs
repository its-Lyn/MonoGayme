using Microsoft.Xna.Framework;

namespace MonoGayme.Utilities;

public static class Collision {
    public static bool CheckRectPoint(Vector2 point, Rectangle rect)
        => point.X >= rect.X && point.X <= rect.X + rect.Width && point.Y >= rect.Y && point.Y <= rect.Y + rect.Height;
}
