
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGayme.Controllers;

namespace MonoGayme.Entities;

public abstract class Entity(Game windowData, int zIndex = 0)
{
    public readonly int ZIndex = zIndex;
    protected readonly Game WindowData = windowData;

    public readonly ComponentController Components = new ComponentController();

    public Vector2 Position = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;

    public abstract void LoadContent();

    public abstract void Update(GameTime time);
    public abstract void Draw(SpriteBatch batch, GameTime time);
}
