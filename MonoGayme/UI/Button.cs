using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGayme.Components;

namespace MonoGayme.UI;

public abstract class Button(bool ignoreMouse) : IElement
{
    public Action? OnClick;

    public Vector2 Position;
    public Color Colour { get; set; }

    public abstract void Update(Vector2 mouse);
    public abstract void Draw(SpriteBatch batch, Camera2D? camera);

    public void RunAction()
    {
        if (OnClick is null)
        {
            Console.Error.WriteLine("Button has no OnClick callback! Skipping.");
            return;
        }

        OnClick?.Invoke();
    }
}
