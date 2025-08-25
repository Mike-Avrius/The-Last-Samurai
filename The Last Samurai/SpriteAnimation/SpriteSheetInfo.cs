using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

/// <summary>
/// Contains metadata about a sprite sheet texture
/// Stores texture reference and grid layout information
/// Used by SpriteSheet class to calculate sprite regions
/// </summary>
public class SpriteSheetInfo
{
    /// <summary>
    /// The texture containing the sprite sheet
    /// </summary>
    public Texture2D texture { get; set; }
    
    /// <summary>
    /// Number of columns in the sprite sheet grid
    /// </summary>
    public int columns { get; set; }
    
    /// <summary>
    /// Number of rows in the sprite sheet grid
    /// </summary>
    public int rows { get; set; }
}