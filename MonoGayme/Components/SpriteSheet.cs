using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGayme.Components;

public class SpriteSheet : Component
{
    private Texture2D _sprite;

    private Vector2 _origin;

    private Vector2 _frameSize;
    private Vector2 _frameCount;

    private Rectangle _source;

    private bool _wrap;

    public Vector2 Position;

    public SpriteSheet(Texture2D sprite, Vector2 frameCount, Vector2 position, bool wrap = false, Vector2? origin = null)
    { 
        _origin = origin ?? Vector2.Zero;
        _frameCount = frameCount;

        _frameSize = _frameSize = new Vector2(sprite.Width / frameCount.X, sprite.Height / frameCount.Y);
        _source = new Rectangle(0, 0, (int)_frameSize.X, (int)_frameSize.Y);

        _wrap = wrap;

        Position = position;

        _sprite = sprite;
    }

    public void Draw(SpriteBatch batch, Camera2D? camera = null)
    {
        batch.Draw(_sprite, camera is null ? Position : camera.ScreenToWorld(Position), _source, Color.White, 0, _origin, 1, SpriteEffects.None, 0);
    }

    /// <summary>
    /// Increment the cell by its Y axis.
    /// </summary>
    /// <param name="increment">How many times should it should be incremented by.</param>
    public void IncrementX(int increment = 1) {}

    /// <summary>
    /// Decrement the cell by its X axis.
    /// </summary>
    /// <param name="decrement">How many times it should be decremented by.</param>
    public void DecrementX(int decrement = 1) {}

    /// <summary>
    /// Increment the cell by it's Y axis.
    /// </summary>
    /// <param name="increment">How many times it should be incremented by.</param>
    public void IncrementY(int increment = 1)
    {
        int value = (int)(increment * _frameSize.Y);
        if (_source.Y + value > _frameSize.Y)
        {
            if (_wrap)
            {
                _source.Y = 0;
                return;
            }
    
            value = (int)_frameSize.Y;
        }

        _source.Y += value;
    }

    /// <summary>
    /// Decrement the cell by it's Y axis.
    /// </summary>
    /// <param name="decrement">How many times it should be decremented by.</param>
    public void DecrementY(int decrement = 1)
    {
        int value = (int)(decrement * _frameSize.Y);
        if (_source.Y - value < 0)
        {
            if (_wrap)
            {
                _source.X = (int)_frameSize.X;
                return;
            }

            value = 0;
        }

        _source.Y -= value;
    }
}
