using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

public class Text : IUpdateable, IDrawable
{
    // Text rendering properties
    private SpriteFont font;                       // Font used for rendering the text
    
    // Text content and appearance
    public string text;                            // The text string to display
    public Vector2 position = Vector2.Zero;        // World position of the text
    public Vector2 scale = Vector2.One;            // Scale factors for width and height
    public float rotation = 0.0f;                  // Rotation in radians
    public Color textColor = Color.White;          // Color of the rendered text

    // Text positioning and rendering
    private Vector2 textCenter;                    // Calculated center point of the text for rotation
    public float layerIndex;                       // Rendering layer (higher values render on top)
    
   
    /// Creates a new text object with the specified font
    public Text(SpriteFont font)
    {
        this.font = font;
    }

   
    // Updates the text object each frame - called by the scene management system
    // Calculates the center point for rotation and positioning
    public virtual void Update(GameTime gameTime)
    {
        // Calculate the center point of the text for rotation origin
        textCenter = font.MeasureString(text) * 0.5f;  
    }

    /// <summary>
    /// Renders the text to the screen using the provided SpriteBatch
    /// </summary>
    /// <param name="_spriteBatch">SpriteBatch for rendering text</param>
    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.DrawString(
            font,
            text,
            position,
            textColor,
            MathHelper.ToRadians(rotation),
            textCenter,
            scale,
            SpriteEffects.None,
            layerIndex
        );
    }
}