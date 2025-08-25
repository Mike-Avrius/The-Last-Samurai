using Microsoft.Xna.Framework;

namespace TopDownFinalPR;

public class Button: Sprite, IUIElements
{
    // Delegate and event for button click handling
    public delegate void ButtonClickHandler();
    public event ButtonClickHandler OnClick;
    
    
    /// Creates a new button with the specified sprite texture
    public Button(string spriteName) : base(spriteName)
    {
        // Set high layer index to render above other UI elements
        layerIndex = 0.9f;
        scale = new Vector2(0.7f, 0.8f);
    }
    
    // Updates button state and checks for mouse input
    // Called every frame to detect clicks and trigger events
   
    public void Update()
    {
        // Create rectangle bounds around the button's sprite for collision detection
        Rectangle bounds = new Rectangle(
            (int)(position.X - texture.Width / 2f),
            (int)(position.Y - texture.Height / 2f),
            texture.Width,
            texture.Height
        );
        
        // Check if mouse position is above button and left mouse button is clicked
        if (bounds.Contains(MouseController.MousePosition) && MouseController.IsLeftClick)
        {
            OnClick?.Invoke();
        }
    }
}