using Microsoft.Xna.Framework;

namespace MonoGayme.Components.Colliders;

public class RectCollider
{
    public Rectangle Bounds;

    public Vector2 GetCentre()
        => new Vector2((Bounds.X + Bounds.Width) / 2, (Bounds.Y + Bounds.Height) / 2);
}
