using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace TopDownFinalPR;

/// <summary>
/// Manages loading, storing, and accessing game textures and sprites
/// Provides centralized texture management and loading functionality
/// Uses ContentManager for loading XNA content files
/// </summary>
public class SpriteManager
{
    // Content manager for loading XNA content files
    static ContentManager content;
    
    // Dictionary storing all loaded sprites with their metadata
    static Dictionary<string, SpriteSheetInfo> sprites = new Dictionary<string, SpriteSheetInfo>();
    
    /// <summary>
    /// Initializes the SpriteManager with a ContentManager instance
    /// </summary>
    /// <param name="contentManager">XNA ContentManager for loading textures</param>
    public SpriteManager(ContentManager contentManager)
    {
        content = contentManager;
    }
    
    /// <summary>
    /// Loads and registers a new sprite texture with the specified parameters
    /// </summary>
    /// <param name="spriteName">Unique identifier for the sprite</param>
    /// <param name="fileName">File path relative to Content directory</param>
    /// <param name="columns">Number of columns in the sprite sheet (default: 1)</param>
    /// <param name="rows">Number of rows in the sprite sheet (default: 1)</param>
    public static void AddSprite(string spriteName, string fileName, int columns = 1, int rows = 1)
    {
        try
        {
            // Create new sprite info and load texture
            sprites[spriteName] = new SpriteSheetInfo();
            sprites[spriteName].texture = content.Load<Texture2D>(fileName);
            
            // Set sprite sheet dimensions
            sprites[spriteName].columns = columns;
            sprites[spriteName].rows = rows;
        }
        catch (Exception ex)
        {
            // Log error if sprite loading fails
            Console.WriteLine($"ERROR loading sprite {spriteName}: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a previously loaded sprite by name
    /// </summary>
    /// <param name="spriteName">Name of the sprite to retrieve</param>
    /// <returns>SpriteSheetInfo containing texture and metadata</returns>
    public static SpriteSheetInfo GetSprite(string spriteName)
    {
        return sprites[spriteName];
    }
}