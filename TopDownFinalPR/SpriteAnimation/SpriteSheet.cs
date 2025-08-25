using Microsoft.Xna.Framework;

namespace TopDownFinalPR;

/// <summary>
/// Provides access to individual sprite regions within a sprite sheet texture
/// Calculates source rectangles for sprite sheet animation frames
/// Used by the Animation class to extract individual frames from texture atlases
/// </summary>
public class SpriteSheet
{
    /// <summary>
    /// The sprite sheet information containing texture and dimensions
    /// </summary>
    public SpriteSheetInfo SpriteSheetInfo { get; }

    /// <summary>
    /// Creates a new sprite sheet with the specified information
    /// </summary>
    /// <param name="spriteSheetInfo">Information about the sprite sheet texture and layout</param>
    public SpriteSheet(SpriteSheetInfo spriteSheetInfo)
    {
        this.SpriteSheetInfo = spriteSheetInfo;
    }
    
    /// <summary>
    /// Indexer that returns a source rectangle for a specific sprite in the sheet
    /// </summary>
    /// <param name="index_x">Column index (0-based)</param>
    /// <param name="index_y">Row index (0-based)</param>
    /// <returns>Rectangle representing the source region for the specified sprite</returns>
    public Rectangle this[int index_x, int index_y]
    {
        get
        {
            // Calculate the top-left corner of the sprite region
            Point location = new Point(
                (int)(SpriteSheetInfo.texture.Width * ((float)index_x / SpriteSheetInfo.columns)),
                (int)(SpriteSheetInfo.texture.Height * ((float)index_y / SpriteSheetInfo.rows))
            );
            
            // Calculate the size of each individual sprite
            Point size = new Point(
                (int)(SpriteSheetInfo.texture.Width * (1.0f / SpriteSheetInfo.columns)),
                (int)(SpriteSheetInfo.texture.Height * (1.0f / SpriteSheetInfo.rows))
            );
            
            // Return the source rectangle for the specified sprite
            return new Rectangle(location, size);
        }
    }
}