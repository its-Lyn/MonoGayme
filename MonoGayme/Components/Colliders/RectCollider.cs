using Microsoft.Xna.Framework;
using MonoGayme.Utilities;

namespace MonoGayme.Components.Colliders;

public class RectCollider : Component
{
    public Rectangle Bounds;

    public bool Enabled = true;

    public Vector2 GetCentre()
        => new Vector2((Bounds.X + Bounds.Width) / 2, (Bounds.Y + Bounds.Height) / 2);

    public bool Collides(Rectangle other)
        => Collision.CheckRects(Bounds, other) && Enabled;

    public bool Collides(RectCollider other)
        => Collides(other.Bounds) && Enabled;
}
