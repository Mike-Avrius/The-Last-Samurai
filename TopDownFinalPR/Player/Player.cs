using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownFinalPR;

public class Player : Animation, IUIElements
{
    // Movement and gameplay constants
    private int speed;                    
    private int maxHealth;                
    private int currentHealth;                  

    // Collision detection
    public Collider collider;                   
    
    // Position tracking for collision resolution
    private Vector2 prevPos;                    

    // Input state tracking
    private KeyboardState prevKey;              
    private MouseState prevMouseState;          
    
    // Animation state
    private bool wasMoving = false;            
    
    /// Initializes the player with default settings and creates collision detection
    public Player() : base("Player_Idle")
    {
        speed = 300;
        maxHealth = 100;
        // Set initial position and appearance
        position = Game1.ScreenCenter;
        scale = new Vector2(1.2f, 1.2f);
        currentHealth = maxHealth;
        layerIndex = 0.5f;
        
        // Set initial idle animation
        SetAnim("Player_Idle", true, 10); 
        
        // Create collider for enemy interactions
        collider = SceneManager.Create<Collider>();
        collider.isTrigger = true;
        collider.visible = false; // Hide the collider visually
        PrintHealth();
    }

    
    // Handles trigger events when player overlaps with other objects
    public void OnTrigger(object o)
    {
        if (o is Enemy enemy)
        {
            // Play collection sound and remove enemy
            AudioManager.PlaySoundEffect("collect");
            SceneManager.Remove(enemy.collider);
            SceneManager.Remove(enemy);
        }
    }
    
    // Handles collision events when player hits solid objects
    public void OnCollision(object o)
    {
        AudioManager.PlaySoundEffect("bounce");
        position = prevPos; // Rollback to previous position
    }
    
    // Applies damage to the player and handles death logic
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        PrintHealth();
        
        // Ensure health doesn't go below 0
        if (currentHealth < 0) currentHealth = 0;
        
        if (currentHealth <= 0)
        {
            // Player is dead
            AudioManager.PlaySoundEffect("death_sound");
            SetAnim("Player_Dead", false, 20);
            GameManager.GameIsOver();
        }
        else
        {
            // Player took damage but survived
            AudioManager.PlaySoundEffect("damage_sound");
            SetAnim("Player_Hurt", false, 10);
        }
    }
    
    // Returns the current player health
    public int GetHealth()
    {
        return currentHealth;
    }
    
    // Updates the health display text
    public void PrintHealth()
    {
        Game1.Instance.healthText.text = $"Health: {currentHealth}";
    }
    
    // Checks if the player is currently alive
    public bool IsAlive()
    {
        bool alive = currentHealth > 0;
        return alive;
    }
    
   
    // Main update method called every frame to handle input, movement, and game logic
    public override void Update(GameTime gameTime)
    {
        // Don't update if player is dead
        if (!IsAlive())
        {
            return;
        }
        
        // Store previous position for collision resolution
        prevPos = position;
        
        // Get current input states
        KeyboardState state = Keyboard.GetState();
      
        // Handle mouse shooting - detect left click
        MouseState currentMouseState = Mouse.GetState();
        if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
        {
            SetAnim("Player_Shot", false, 30);
            ShootArrow();
        }
        prevMouseState = currentMouseState;
        
        // Handle keyboard movement
        bool isMoving = false;
        
        // Right movement (D key)
        if (state.IsKeyDown(Keys.D))
        {
            position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            effect = SpriteEffects.None;
            isMoving = true;
        }
        // Left movement (A key)
        if (state.IsKeyDown(Keys.A))
        {
            position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            effect = SpriteEffects.FlipHorizontally;
            isMoving = true;
        }
        // Up movement (W key)
        if (state.IsKeyDown(Keys.W))
        {
            position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            isMoving = true;
        }
        // Down movement (S key)
        if (state.IsKeyDown(Keys.S))
        {
            position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            isMoving = true;
        }
        
        // Update animation based on movement state
        if (isMoving != wasMoving)
        {
            if (isMoving) SetAnim("Player_Walk", true, 30);
            else          SetAnim("Player_Idle", true, 10);

            wasMoving = isMoving;
        }
        
        // Audio control keys
        if (state.IsKeyDown(Keys.M) && prevKey.IsKeyUp(Keys.M))
        {
            AudioManager.MuteAudio();
        }
        if (state.IsKeyDown(Keys.V) && prevKey.IsKeyUp(Keys.M))
        {
            AudioManager.UnMuteAudio();
        }

        // Store current input state for next frame
        prevKey = state;
        base.Update(gameTime);

        // Update collider position to match player
        if (collider != null)
        {
            collider.rect = rect;
        }
        
        // Constrain player position to screen boundaries
        position = ScreenBounds.ClampToScreen(position, new Vector2(rect.Width, rect.Height));
    }
    
    
    // Creates and fires an arrow from player position toward mouse cursor
    private void ShootArrow()
    {
        // Check if player is alive before shooting
        if (!IsAlive())
        {
            return;
        }
        
        // Get mouse position for aiming
        MouseState mouseState = Mouse.GetState();
        Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
        
        // Clamp mouse position to screen boundaries
        mousePosition.X = Math.Max(0, Math.Min(mousePosition.X, Game1.ScreenCenter.X * 2));
        mousePosition.Y = Math.Max(0, Math.Min(mousePosition.Y, Game1.ScreenCenter.Y * 2));
       
        // Play shooting sound effect
        AudioManager.PlaySoundEffect("shoot_sound");
        
        // Create arrow from player position to mouse position
        Arrow arrow = new Arrow(position, mousePosition);
    }
}