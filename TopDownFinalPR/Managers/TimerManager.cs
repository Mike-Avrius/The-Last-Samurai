using System;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

public class TimerManager: Text, IUIElements
{
    // Timer state variables
    private double timer;                         
    private bool isRunning;                        
    private bool timeIsOver;                       
    
    // UI and display properties
    private Sprite target;                        
    private SpriteFont font;                       
    private int lastWholeSecond;                   
    
    
    /// Initializes the timer with default values and UI settings
    public TimerManager(SpriteFont font) : base(font)
    {
        this.font = font;
        timer = 60f;                                // Initial timer value
        isRunning = false;                         // Timer starts paused
        textColor = Color.Black;                    // Black text color
        scale = new Vector2(1f, 1f);               // Default scale
        layerIndex = 1f;                           // Render on top layer
        timeIsOver = false;                        // Timer hasn't expired yet
        lastWholeSecond = (int)Math.Ceiling(timer); // Initialize last second tracking
    }
    
 
    /// Resets the timer to 60 seconds and starts the countdown
    /// Also creates a new player when called
    public void ResetTimer()
    {
        timer = 60f;                               // Set timer to 60 seconds
        isRunning = true;                          // Start the countdown
        timeIsOver = false;                        // Reset expiration flag
        lastWholeSecond = (int)Math.Ceiling(timer); // Reset second tracking
        PlayerManager.CreatePlayer();               // Create new player
    }
        
    
    // Positions the timer text on a specified sprite target
    public void PutTimeOnClock(Sprite target)
    {
        this.target = target;
        UpdateText(timer.ToString());
    }
    
   
    // Updates the displayed text and recalculates position
    private void UpdateText(string newText)
    {
        text = newText;
        UpdatePosition();
    }
    

    // Calculates and updates the position of the timer text relative to its target
    private void UpdatePosition()
    {
        if (target == null || font == null || string.IsNullOrEmpty(text))
            return;

        // Calculate text dimensions and position
        Vector2 textSize = font.MeasureString(text) * scale;
        float yOffset = 10f;                       // Small vertical offset for better positioning

        position = target.position - textSize / 2f + new Vector2(0, yOffset);
    }

    
    // Returns whether the timer has expired
    public bool TimerStatus()
    {
        return timeIsOver;
    }

   
    // Forces the timer to zero and marks it as expired
    public void SetTimerToZero()
    {
        timer = 0f;
        timeIsOver = true;
    }
    
    
    // Main update method called every frame to update timer and trigger events
    public override void Update(GameTime gameTime)
    {
        if (isRunning)
        {
            // Decrease timer by elapsed time
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            int currentWholeSecond = (int)Math.Ceiling(timer);

            // Spawn enemies every 2 seconds when timer is running
            if (currentWholeSecond < lastWholeSecond && timer > 0 && currentWholeSecond % 2 == 0)
            {
                for (int i = 0; i < GameManager.dificulty; i++)
                {
                    EnemyManager.SpawnEnemy<TenguEnemy>();
                }
            }

            lastWholeSecond = currentWholeSecond;
            
            // Check if timer has expired
            if (timer <= 0)
            {
                isRunning = false;
                timer = 0;
                timeIsOver = true;
                GameManager.GameIsOver();
            }
            
            // Update displayed text
            UpdateText($"{MathF.Ceiling((float)timer)}");
        }
    }

    
    // Renders the timer text to the screen
    public new void Draw(SpriteBatch _spriteBatch)
    {
        base.Draw(_spriteBatch);
    }
}