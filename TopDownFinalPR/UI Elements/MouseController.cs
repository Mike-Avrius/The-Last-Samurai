using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace TopDownFinalPR;

public static class MouseController
{
    // Current position of the mouse cursor on screen
    public static Point MousePosition { get; private set; }
    
    // True only on the frame when the left mouse button was just pressed
    // Used for detecting single clicks without holding
    public static bool IsLeftClick { get; private set; }

    // Previous mouse state for detecting state changes
    private static MouseState _previousMouseState;

    
    // Updates mouse state each frame to detect position changes and button clicks
    // Must be called every frame from the main game loop
    public static void Update()
    {
        MouseState currentMouseState = Mouse.GetState();

        // Update current mouse position
        MousePosition = currentMouseState.Position;
        
        // Detect left click (button pressed this frame but was up last frame)
        IsLeftClick = currentMouseState.LeftButton == ButtonState.Pressed &&
                      _previousMouseState.LeftButton == ButtonState.Released;

        // Store current state for next frame comparison
        _previousMouseState = currentMouseState;
    }
}