using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGayme.Components;

namespace MonoGayme.UI;

public interface IElement 
{
    Color Colour { get; set; }
    
    void RunAction();

    void Update(Vector2 mouse);
    void Draw(SpriteBatch batch, Camera2D? camera);
}