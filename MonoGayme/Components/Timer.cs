using Microsoft.Xna.Framework;
using System;

namespace MonoGayme.Components;

/// <summary>
///  Create a new Timer.
/// </summary>
/// <param name="time">The time untill the timeout in seconds.</param>
public class Timer(float time, bool enabled, bool oneShot)
{
    private float _timer = 0;

    public Action? OnTimeOut;

    public void Cycle(GameTime gameTime)
    {
        if (enabled)
        { 
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer >= time)
            {
                _timer = 0;
                OnTimeOut?.Invoke();

                if (oneShot)
                {
                    enabled = false;
                }
            }
        } 
    }

    public void Stop() => enabled = false;

    public void Start() => enabled = true;

    public void Reset() => _timer = 0;

    public float TimeLeft
    {
        get
        {
            if (enabled)
            { 
                return Math.Max(0, time - _timer);
            }

            return 0;
        }
    }

    public float Time
    {
        get => time;
        set
        {
            if (value < 0) return;
            time = value;
        }
    }
}
