using Microsoft.Xna.Framework;

namespace TopDownFinalPR;

public static class ScreenBounds
{
    // Gets the total screen width in pixels
    public static int ScreenWidth => (int)Game1.ScreenCenter.X * 2;
    
    // Gets the total screen height in pixels
    public static int ScreenHeight => (int)Game1.ScreenCenter.Y * 2;

  
    // Creates a rectangle representing the entire screen area
    public static Rectangle GetScreenRect()
    {
        return new Rectangle(0, 0, ScreenWidth, ScreenHeight);
    }
    
    // Clamps a position to ensure it stays within screen boundaries
    // Takes into account the size of the object being positioned
    public static Vector2 ClampToScreen(Vector2 position, Vector2 size)
    {
        // Clamp X coordinate to keep object within screen bounds
        float x = MathHelper.Clamp(position.X, size.X / 2, ScreenWidth - size.X / 2);
        
        // Clamp Y coordinate to keep object within screen bounds
        float y = MathHelper.Clamp(position.Y, size.Y / 2, ScreenHeight - size.Y / 2);
        
        return new Vector2(x, y);
    }
}