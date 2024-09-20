
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGayme.Entities;

public abstract class Entity(Game windowData, int zIndex = 0) {
    public int ZIndex = zIndex;
    protected Game WindowData = windowData;

    public abstract void LoadContent();

    public abstract void Update(GameTime time);
    public abstract void Draw(SpriteBatch batch, GameTime time);
}
