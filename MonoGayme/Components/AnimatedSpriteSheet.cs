using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGayme.Components;

public class AnimatedSpriteSheet : Component
{
    private Vector2 _origin;
    private Vector2 _frameSize;
    private Texture2D _sprite;

    private Vector2 _frameCount; 

    private Rectangle _source;

    private float _speed;
    private float _frameTimer = 0;
    private int _frame = 0;

    public bool Finished = true;
    public bool Loop;

    public Action? OnSheetFinished;

    public AnimatedSpriteSheet(Texture2D sprite, Vector2 size, float speed, bool loop = false, Vector2? origin = null)
    {
        _origin = origin ?? Vector2.Zero;
        
        _frameCount = size; 
        _sprite = sprite;

        _speed = speed;

        _frameSize = new Vector2(sprite.Width / size.X, sprite.Height / size.Y);
        _source = new Rectangle(0, 0, (int)_frameSize.X, (int)_frameSize.Y);

        Loop = loop;
    }

    public void CycleAnimation(GameTime time)
    {
        _frameTimer += (float)time.ElapsedGameTime.TotalSeconds;
        if (_frameTimer >= _speed) {
            _frameTimer = 0;

            _frame++;
            if (_frame >= _frameCount.X) {
                _frame = 0;

                if (!Loop) {
                    Finished = true;
                }

                OnSheetFinished?.Invoke();
            }

            _source.X = (int)(_frame * _frameSize.X);
        }
    }

    public void Draw(SpriteBatch batch, Vector2 pos, bool flipped = false)
    {
        batch.Draw(_sprite, pos, _source, Color.White, 0f, _origin, 1f, flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
    }
}