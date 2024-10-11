using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGayme.States;

/// <summary>
/// Creates a new Game State.
/// </summary>
/// <param name="windowData">Instance to the main Game class for data handling.</param>
public abstract class State(Game windowData)
{
    protected readonly Game WindowData = windowData;

    public abstract void LoadContent();

    public abstract void Update(GameTime time);
    public abstract void Draw(GameTime time, SpriteBatch batch);
}
