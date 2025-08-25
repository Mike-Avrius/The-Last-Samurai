using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

/// <summary>
/// Interface for objects that can be rendered to the screen
/// Implemented by all visual game objects that need drawing
/// Used by SceneManager to render all visible objects each frame
/// </summary>
public interface IDrawable
{
    /// <summary>
    /// Renders the object to the screen using the provided SpriteBatch
    /// Called every frame by the scene management system
    /// </summary>
    /// <param name="_spriteBatch">SpriteBatch for rendering sprites and textures</param>
    void Draw(SpriteBatch _spriteBatch);
}