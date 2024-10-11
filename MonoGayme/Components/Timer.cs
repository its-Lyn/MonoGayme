using Microsoft.Xna.Framework;
using System;

namespace MonoGayme.Components;

/// <summary>
///  Create a new Timer.
/// </summary>
public class Timer : Component
{
    private float _timer;
    private float _time;
    private readonly bool _oneShot;

    public Action? OnTimeOut;

    public Timer(float time, bool enabled, bool oneShot, string? name = null)
    {
        _time = time;
        Enabled = enabled;
        _oneShot = oneShot;

        Name = name;
    }

    public void Cycle(GameTime gameTime)
    {
        if (!Enabled) return;
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (!(_timer >= _time)) return;
        _timer = 0;
        if (_oneShot)
            Enabled = false;

        OnTimeOut?.Invoke();
    }

    public void Stop() => Enabled = false;

    public void Start() => Enabled = true;

    public void Reset() => _timer = 0;

    public bool Enabled { get; private set; }

    public float TimeLeft
    {
        get
        {
            if (Enabled)
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
