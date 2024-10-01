using Microsoft.Xna.Framework;
using System;

namespace MonoGayme.Components;

/// <summary>
///  Create a new Timer.
/// </summary>
/// <param name="time">The time untill the timeout in seconds.</param>
public class Timer : Component
{
    private float _timer = 0;
    private float _time;
    private bool _enabled;
    private bool _oneShot;

    public Action? OnTimeOut;

    public Timer(float time, bool enabled, bool oneShot, string? name = null)
    {
        _time = time;
        _enabled = enabled;
        _oneShot = oneShot;

        Name = name;
    }

    public void Cycle(GameTime gameTime)
    {
        if (_enabled)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer >= _time)
            {
                _timer = 0;
                if (_oneShot)
                {
                    _enabled = false;
                }

                OnTimeOut?.Invoke();
            }
        }
    }

    public void Stop() => _enabled = false;

    public void Start() => _enabled = true;

    public void Reset() => _timer = 0;

    public bool Enabled => _enabled;

    public float TimeLeft
    {
        get
        {
            if (_enabled)
            {
                return Math.Max(0, _time - _timer);
            }

            return 0;
        }
    }

    public float Time
    {
        get => _time;
        set
        {
            if (value < 0) return;
            _time = value;
        }
    }
}
