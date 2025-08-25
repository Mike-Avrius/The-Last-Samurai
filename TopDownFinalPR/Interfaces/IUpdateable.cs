using Microsoft.Xna.Framework;

namespace TopDownFinalPR;

/// <summary>
/// Interface for objects that need to be updated each frame
/// Implemented by game objects that have dynamic behavior or state changes
/// Used by SceneManager to update all active game objects
/// </summary>
public interface IUpdateable
{
    /// <summary>
    /// Updates the object's state for the current frame
    /// Called every frame by the scene management system
    /// </summary>
    /// <param name="gameTime">Provides timing information for frame-rate independent updates</param>
    void Update(GameTime gameTime);
}