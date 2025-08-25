using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

/// <summary>
/// Manages text elements that are attached to UI objects
/// Provides functionality to attach text to buttons and panels with automatic positioning
/// Inherits from Text for rendering and implements IUIElements for UI integration
/// </summary>
public class TextManager: Text, IUIElements
{
    // Target object for text positioning
    private Sprite target;                         // The sprite object to attach text to
    
    // Font and rendering properties
    private SpriteFont font;                       // Font for rendering the text
    
    // Static collection for managing all text managers
    private static List<TextManager> _texts = new List<TextManager>();
    
    /// <summary>
    /// Creates a new text manager with the specified font
    /// </summary>
    /// <param name="font">Font to use for rendering the text</param>
    public TextManager(SpriteFont font) : base(font)
    {
        this.font = font;
        layerIndex = 1f;                           // Render on top layer
    }

    /// <summary>
    /// Attaches text to a button with specified text, color, and optional scale
    /// </summary>
    /// <param name="target">Button to attach text to</param>
    /// <param name="text">Text string to display</param>
    /// <param name="color">Color of the text</param>
    /// <param name="scale">Optional scale factor for the text</param>
    public void Attach(Button target, string text, Color color, Vector2? scale = null)
    {
        this.target = target;
        this.text = text;
        textColor = color;
        this.scale = scale ?? Vector2.One;

        UpdatePosition();
    }
    
    /// <summary>
    /// Places text on a menu panel with specified text, color, and optional scale
    /// </summary>
    /// <param name="target">Menu panel to place text on</param>
    /// <param name="text">Text string to display</param>
    /// <param name="color">Color of the text</param>
    /// <param name="scale">Optional scale factor for the text</param>
    public void PutOnPanel(MenuPanels target, string text, Color color, Vector2? scale = null)
    {
        this.target = target;
        this.text = text;
        textColor = color;
        this.scale = scale ?? Vector2.One;

        UpdatePosition();
    }
    
    /// <summary>
    /// Calculates and updates the position of the text relative to its target object
    /// Centers the text on the target object's position
    /// </summary>
    private void UpdatePosition()
    {
        if (target == null || font == null || string.IsNullOrEmpty(text))
            return;
            
        position = target.position;
    }

    /// <summary>
    /// Updates the text manager each frame - called by the scene management system
    /// Recalculates text position to follow the target object
    /// </summary>
    /// <param name="gameTime">Provides timing information for frame-rate independent updates</param>
    public override void Update(GameTime gameTime)
    {
        UpdatePosition();
        base.Update(gameTime);
    }
    
    public void PrintOnScreen(string text, Color color, Vector2? scale = null)
    {
        this.text = text;
        textColor = color;
        this.scale = scale ?? Vector2.One;

        UpdatePosition();
    }
    
}