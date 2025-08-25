using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TopDownFinalPR;

/// <summary>
/// Base sprite class that provides fundamental rendering and positioning functionality
/// All visual game objects inherit from this class
/// Implements IUpdateable and IDrawable for integration with the scene management system
/// </summary>
public class Sprite : IUpdateable, IDrawable
{
    /// <summary>
    /// Defines the origin point for sprite positioning and rotation
    /// </summary>
    public enum OriginPosition
    {
        TopLeft,    // Origin at top-left corner of sprite
        Center      // Origin at center of sprite (default)
    }
    
    // Transform properties
    public Vector2 position = Vector2.Zero;         // World position of the sprite
    public Vector2 scale = Vector2.One;             // Scale factors for width and height
    public float rotation = 0.0f;                   // Rotation in radians
    public float layerIndex;                        // Rendering layer (higher values render on top)
    public OriginPosition originPosition = OriginPosition.Center; // Origin point for transformations
    
    // Internal rendering properties
    protected Vector2 origin = Vector2.Zero;         // Calculated origin point
    protected Texture2D texture;                     // The texture to render
    protected Rectangle? sourceRectangle = null;     // Optional source rectangle for texture regions
    protected SpriteEffects effect = SpriteEffects.None; // Flip effects for the sprite

    // Collision and bounds
    public Rectangle rect { get; set; }              // Bounding rectangle for collision detection

    /// <summary>
    /// Creates a new sprite with the specified texture
    /// </summary>
    /// <param name="textureName">Name of the texture to load from SpriteManager</param>
    public Sprite(string textureName)
    {
        // Load texture and set up default properties
        this.texture = SpriteManager.GetSprite(textureName).texture;
        origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
        rect = GetDestRectangle(texture.Bounds);
    }

    /// <summary>
    /// Sets the origin point for the sprite based on the specified position
    /// </summary>
    /// <param name="originPosition">Where to place the origin point</param>
    protected virtual void SetOrigin(OriginPosition originPosition)
    {
        switch (originPosition)
        {
            case OriginPosition.TopLeft:
                origin = Vector2.Zero;
                break;
                
            case OriginPosition.Center:
                origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
                break;
        }
    }

    /// <summary>
    /// Updates the sprite each frame - called by the scene management system
    /// </summary>
    /// <param name="gameTime">Provides timing information for frame-rate independent updates</param>
    public virtual void Update(GameTime gameTime)
    {
        SetOrigin(originPosition);
    }

    /// <summary>
    /// Calculates the destination rectangle for rendering based on position, scale, and origin
    /// </summary>
    /// <param name="rect">Source rectangle (usually the full texture bounds)</param>
    /// <returns>Destination rectangle in world coordinates</returns>
    protected Rectangle GetDestRectangle(Rectangle rect)
    {
        // Calculate scaled dimensions
        int width = (int)(rect.Width * scale.X);
        int height = (int)(rect.Height * scale.Y);

        // Calculate position accounting for origin offset
        int pos_x = (int)(position.X - origin.X * scale.X);
        int pos_y = (int)(position.Y - origin.Y * scale.Y);

        return new Rectangle(pos_x, pos_y, width, height);
    }

    /// <summary>
    /// Renders the sprite to the screen using the provided SpriteBatch
    /// </summary>
    /// <param name="_spriteBatch">SpriteBatch for rendering</param>
    public virtual void Draw(SpriteBatch _spriteBatch)
    {
        // Draw the sprite with all current transform properties
        _spriteBatch.Draw(
            texture,
            position,
            sourceRectangle,
            Color.White,
            rotation,
            origin,
            scale,
            effect,
            layerIndex
        );
    }
    
    /// <summary>
    /// Flips the sprite horizontally based on the specified direction
    /// </summary>
    /// <param name="lookLeft">True to flip left, false to face right</param>
    public void Flip(bool lookLeft)
    {
        effect = lookLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
    }
}