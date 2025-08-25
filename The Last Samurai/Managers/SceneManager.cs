using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

public class SceneManager : IUpdateable, IDrawable
{
    // Collections for managing different types of game objects
    private static List<IUpdateable> updateables = new List<IUpdateable>();    // Objects that need updates each frame
    private static List<IDrawable> drawables = new List<IDrawable>();          // Objects that need rendering each frame
    
    // Singleton instance
    private static SceneManager instance;

    
    /// Singleton instance accessor - creates instance if it doesn't exist
    public static SceneManager Instance
    {
        get
        {
            if (instance == null)
                instance = new SceneManager();
            
            return instance;
        }
    }

    
    /// Creates a new object of type T and automatically adds it to the scene
    public static T Create<T>() where T : IDrawable, new()
    {
        T item = new T();
        Add(item);
        
        return item;
    }

    
    // Adds an object to the scene management system
    // Automatically categorizes objects as updateable and/or drawable
    public static void Add<T>(T item) where T: IDrawable
    {
        // Add to updateables list if the object implements IUpdateable
        if (item is IUpdateable updateable)
        {
            updateables.Add(updateable);
        }
        
        // Add to drawables list (all objects must be drawable)
        if (item is IDrawable drawable)
        {
            drawables.Add(drawable);
        }
    }
    
    // Removes an object from the scene management system
    public static void Remove<T>(T item) where T: IDrawable
    {
        // Remove from updateables list if applicable
        if (item is IUpdateable updateable)
        {
            updateables.Remove(updateable);
        }
        
        // Remove from drawables list
        if (item is IDrawable drawable)
        {
            drawables.Remove(drawable);
        }
    }
    
    /// Updates all updateable objects in the scene
    /// Called every frame from the main game loop
    public void Update(GameTime gameTime)
    {
        // Update all objects that need updates
        for (int i = 0; i < updateables.Count; i++)
        {
            updateables[i].Update(gameTime);
        }
    }

    
    // Renders all drawable objects in the scene
    // Called every frame from the main game loop
    public void Draw(SpriteBatch _spriteBatch)
    {
        // Render all objects that need drawing
        foreach (IDrawable drawable in drawables)
        {
            drawable.Draw(_spriteBatch);
        }
    }
    
    
    // Returns a copy of all objects currently in the scene
    // Useful for iteration without modification issues
    public static List<IDrawable> GetAllObjects()
    {
        return new List<IDrawable>(drawables);
    }
}