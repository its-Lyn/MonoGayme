using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGayme.Utilities;

/// <summary>
/// Creates a new renderer that will be drawn in the middle of the screen, scaled.
/// </summary>
public class Renderer(Vector2 size, GraphicsDevice graphics) {
    private RenderTarget2D _renderer = new RenderTarget2D(graphics, (int)size.X, (int)size.Y);

    /// <summary>
    /// Gets the screen scale based on the renderer size.
    /// </summary>
    public float GetScale() 
        => MathF.Min(graphics.Viewport.Width / size.X, graphics.Viewport.Height / size.Y);

    /// <summary>
    /// Sets the render target to the renderer.
    /// Anything after this is called will be drawn inside the renderer.
    /// </summary>
    public void SetRenderer()
        => graphics.SetRenderTarget(_renderer);

    /// <summary>
    /// Sets the render target to the whole screen without drawing the renderer.
    /// </summary>
    public void DisableRenderer() 
        => graphics.SetRenderTarget(null);

    /// <summary>
    /// Sets the render target to the whole screen. 
    /// Then draws the given renderer scaled in the centre so it always fits.
    /// </summary>
    public void DrawRenderer(SpriteBatch batch) {
        graphics.SetRenderTarget(null);
        graphics.Clear(Color.Black);

        float scale = GetScale();
        batch.Begin(samplerState: SamplerState.PointClamp);
            batch.Draw(
                _renderer,
                new Rectangle(
                    (int)((graphics.Viewport.Width - (size.X * scale)) / 2),
                    (int)((graphics.Viewport.Height - (size.Y * scale)) / 2),
                    (int)(size.X * scale),
                    (int)(size.Y * scale)
                ),
                Color.White
            );
        batch.End();
    }

    /// <summary>
    /// Gets the virtual mouse position based on the renderer's internal size.
    /// </summary>
    public Vector2 GetVirtualMousePosition()
    {
        Vector2 position = Mouse.GetState().Position.ToVector2();
        
        float scale = GetScale();
        float virtualMouseX = (position.X - ((graphics.Viewport.Width - (size.X * scale)) / 2)) / scale;
        float virtualMouseY = (position.Y - ((graphics.Viewport.Height - (size.Y * scale)) / 2)) / scale;

        return new Vector2(
            MathHelper.Clamp(virtualMouseX, 0, size.X),
            MathHelper.Clamp(virtualMouseY, 0, size.Y)
        );
    }
}
