using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownFinalPR;

/// <summary>
/// Displays the current mouse cursor position on screen
/// Inherits from Text for rendering and updates position text each frame
/// Useful for debugging and development purposes
/// </summary>
public class MousePosition : Text
{
    /// <summary>
    /// Creates a new mouse position display with the specified font
    /// </summary>
    /// <param name="font">Font to use for rendering the position text</param>
    public MousePosition(SpriteFont font) : base(font)
    {
    }

    /// <summary>
    /// Updates the displayed text to show current mouse coordinates
    /// Called every frame to keep position display current
    /// </summary>
    /// <param name="gameTime">Provides timing information for frame-rate independent updates</param>
    public override void Update(GameTime gameTime)
    {
        // Update text to show current mouse X and Y coordinates
        text = $" ({Mouse.GetState().X} , {Mouse.GetState().Y })";
    }
}