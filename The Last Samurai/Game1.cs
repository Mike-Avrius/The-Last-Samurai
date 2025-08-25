using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownFinalPR;

public class Game1 : Game
{
    private static Game1 instance;
    public static Game1 Instance => instance;
    
    // Managers
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteManager _spriteManager;
    private AudioManager _audioManager;
    private TimerManager _timer;
    public static TimerManager CurrentTimer { get; private set; }
    
    // BackGrounds
    private MenuPanels mainMenuBackGround;
    private MenuPanels gameBackGround;
    private MenuPanels finalBackGround;
    
    // Buttons
    private Button startButton;
    private Button soundButton;
    private Button exitButton;
    private Button returnButton;
    private Button replayButton;
    
    // Texts Fields
    private TextManager gameNameText;
    private TextManager startText;
    private TextManager soundText;
    private TextManager exitText;
    private TextManager instructionText;
    private TextManager restartText;
    private TextManager returnToMenuText2;
    private TextManager gameOverText;
    public TextManager healthText;
    
    // Timer UI sprite Element
    private InGameUIElements clock;
    
    // Controls Text
    private string instruction = "Controls: WASD + LMB";
    
    // Font
    private SpriteFont oswaldFont;
    
    // Screen Center
    public static Vector2 ScreenCenter;
    
    // Player
    private Player player;
    
    //KeyBoard
    private KeyboardState currentKeyboard;
    private KeyboardState previousKeyboard;
    
    public Game1()
    {
        instance = this;
        
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        
        ScreenCenter = new Vector2(_graphics.PreferredBackBufferWidth * 0.5f, _graphics.PreferredBackBufferHeight * 0.5f);
    }

    protected override void Initialize()
    {
        // Initialize Spawn Points for enemies
        EnemyManager.SetSpawnPoints();
        // Activate menu Features from start of game 
        MainMenuUIManager.ActivateMenu();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _audioManager = new AudioManager(Content);
        _spriteManager = new SpriteManager(Content);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        oswaldFont = Content.Load<SpriteFont>("Fonts/OswaldFont");
        
        // Download Sound Effects
        AudioManager.AddSoundEffect("hit_sound", "Audio/hit_sound");
        AudioManager.AddSoundEffect("shoot_sound", "Audio/shoot_sound");
        AudioManager.AddSoundEffect("damage_sound", "Audio/damage_sound");
        AudioManager.AddSoundEffect("death_sound", "Audio/death_sound");
        
        // Download Music and Play at start of the game
        AudioManager.AddSong("theme", "Audio/background_music");
        AudioManager.PlaySong("theme", true, 0.5f);
        
        // Download Background Sprites
       SpriteManager.AddSprite("MainMenuBackground", "Sprites/backGrounds/MainMenuBackground");
       SpriteManager.AddSprite("GameZoneBackGround", "Sprites/backGrounds/GameZoneBackGround");
       SpriteManager.AddSprite("FinalBackGround", "Sprites/backGrounds/FinalBackGround");
       
       // Download UI elements Sprites
       SpriteManager.AddSprite("StartButton", "Sprites/UI/StartButton");
       SpriteManager.AddSprite("SettingButton", "Sprites/UI/SettingButton");
       SpriteManager.AddSprite("ExitButton", "Sprites/UI/ExitButton");
       SpriteManager.AddSprite("Timer", "Sprites/UI/Timer");
       
       // Download Player Sprites and Arrow sprite
       SpriteManager.AddSprite("Player_Idle", "Sprites/Player/Player_Idle", 9, 1);
       SpriteManager.AddSprite("Player_Walk", "Sprites/Player/Player_Walk", 8, 1);
       SpriteManager.AddSprite("Player_Shot", "Sprites/Player/Player_Shot", 14, 1);
       SpriteManager.AddSprite("Player_Hurt", "Sprites/Player/Player_Hurt", 3, 1);
       SpriteManager.AddSprite("Player_Dead", "Sprites/Player/Player_Dead", 5, 1);
       SpriteManager.AddSprite("Arrow", "Sprites/Player/Arrow");
        
       // Download Collider Sprite
       SpriteManager.AddSprite("collider", "Sprites/collider");
       
       // Download Enemy Tengu Sprites
       SpriteManager.AddSprite("EnemyTengu_Idle", "Sprites/Enemies/Tengu/EnemyTengu_Idle", 6, 1);
       SpriteManager.AddSprite("EnemyTengu_Walk", "Sprites/Enemies/Tengu/EnemyTengu_Walk", 8, 1);
       SpriteManager.AddSprite("EnemyTengu_Dead", "Sprites/Enemies/Tengu/EnemyTengu_Dead", 6, 1);
       SpriteManager.AddSprite("EnemyTengu_Attack", "Sprites/Enemies/Tengu/EnemyTengu_Attack", 6, 1);
      
       
       // Create Main Menu UI objects and Add them to MainMenuList + Adding Events
       mainMenuBackGround = new MenuPanels("MainMenuBackground");
       startButton = new Button("StartButton");
                startButton.position = ScreenCenter - new Vector2(0f, -150f);
       startText = new TextManager(oswaldFont);
                startText.Attach(startButton, "START", Color.Black);
       soundButton = new Button("SettingButton");
                soundButton.position = ScreenCenter - new Vector2(-550f, -150f);
       soundText = new TextManager(oswaldFont);
                soundText.Attach(soundButton, "MUSIC", Color.Black);
       exitButton = new Button("ExitButton");
                exitButton.position = ScreenCenter - new Vector2(550f, -150f);
       exitText = new TextManager(oswaldFont);
                exitText.Attach(exitButton,"EXIT", Color.Black);
       gameNameText = new TextManager(oswaldFont);
                 gameNameText.position = ScreenCenter - new Vector2(0f, 250f);
                 gameNameText.PrintOnScreen("The Last Samurai", Color.White, new Vector2(2.8f, 2.8f));
       instructionText = new TextManager(oswaldFont);
                 instructionText.position = ScreenCenter - new Vector2(0f, -450f);
                 instructionText.PrintOnScreen(instruction, Color.White, new Vector2(0.9f, 0.9f));
                 
       startButton.OnClick += GeneralUIManager.ShowGameUI;
       soundButton.OnClick += AudioManager.ChangeMusicStatus;   
       exitButton.OnClick += Exit;
       
        MainMenuUIManager.AddObjToMainMenuList(startButton);
        MainMenuUIManager.AddObjToMainMenuList(startText);
        MainMenuUIManager.AddObjToMainMenuList(soundButton);
        MainMenuUIManager.AddObjToMainMenuList(soundText);
        MainMenuUIManager.AddObjToMainMenuList(exitButton);
        MainMenuUIManager.AddObjToMainMenuList(exitText);
        MainMenuUIManager.AddObjToMainMenuList(gameNameText);
        MainMenuUIManager.AddObjToMainMenuList(instructionText);
        MainMenuUIManager.AddObjToMainMenuList(mainMenuBackGround);
        
        // Create In Game UI objects and Add them to InGameList + Manage Timer    
        clock = new InGameUIElements("Timer");
                clock.position = ScreenCenter - new Vector2(0f, 400f);
                clock.scale = new Vector2(0.2f, 0.2f);
        _timer = new TimerManager(oswaldFont);
                CurrentTimer = _timer; 
                _timer.PutTimeOnClock(clock);
        gameBackGround = new MenuPanels("GameZoneBackGround"); 
                gameBackGround.scale = new Vector2(1.63f, 1.3f);
        healthText = new TextManager(oswaldFont);
                healthText.PutOnPanel(null, "Health: 100", Color.White, new Vector2(1.5f, 1.5f));
                healthText.position = new Vector2(500, 120);
        
        InGameUIManager.AddObjToGameUIList(clock);
        InGameUIManager.AddObjToGameUIList(_timer);
        InGameUIManager.AddObjToGameUIList(gameBackGround);
        InGameUIManager.AddObjToGameUIList(healthText);
        
        
        
        // Create Final UI objects and Add them to FinalList + Manage Events    
        finalBackGround = new MenuPanels("FinalBackGround");
                    finalBackGround.scale = new Vector2(1.63f, 1.3f);
        gameOverText = new TextManager(oswaldFont);
                    gameOverText.PutOnPanel(finalBackGround,"GAME OVER", Color.Red,new Vector2(3f, 3f));
        returnButton = new Button("ExitButton");
                    returnButton.position = ScreenCenter - new Vector2(0f, -400f);
        returnToMenuText2 = new TextManager(oswaldFont);
                    returnToMenuText2.Attach(returnButton, "Menu", Color.Black);
        replayButton = new Button("StartButton");
                    replayButton.position = ScreenCenter - new Vector2(0f, -200f);
        restartText = new TextManager(oswaldFont);
                    restartText.Attach(replayButton, "Try Again", Color.Black);
        
        returnButton.OnClick += GeneralUIManager.ShowMainMenu;
        replayButton.OnClick += GeneralUIManager.ShowGameUI;
        
        EndGameUIManager.AddObjToEndGameUIList(finalBackGround);
        EndGameUIManager.AddObjToEndGameUIList(gameOverText);
        EndGameUIManager.AddObjToEndGameUIList(returnButton);
        EndGameUIManager.AddObjToEndGameUIList(replayButton);
        EndGameUIManager.AddObjToEndGameUIList(restartText);
        EndGameUIManager.AddObjToEndGameUIList(returnToMenuText2);
    }

    protected override void Update(GameTime gameTime)
    {
        
        currentKeyboard = Keyboard.GetState();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            Exit();
 
        // Chechk Mouse Position for clicks
        MouseController.Update();
        MainMenuUIManager.Instance.Update(gameTime);
        SceneManager.Instance.Update(gameTime);
        
        //If menu is Active - Check Buttons
       if (MainMenuUIManager.MenuStatus())
       {
           startButton.Update();
           soundButton.Update();
           exitButton.Update();
           //SetDificulty
           if (currentKeyboard.IsKeyDown(Keys.D1) && previousKeyboard.IsKeyUp(Keys.D1))
           {
               GameManager.SetDificulty(1);
           }
           else if (currentKeyboard.IsKeyDown(Keys.D2) && previousKeyboard.IsKeyUp(Keys.D2))
           {
               GameManager.SetDificulty(2);
           }
           else if (currentKeyboard.IsKeyDown(Keys.D3) && previousKeyboard.IsKeyUp(Keys.D3))
           {
               GameManager.SetDificulty(3);
           }
       }
        // If game is active - Update Ingame objects
       if (GeneralUIManager.isGameUIActive)
       {
           InGameUIManager.Instance.Update(gameTime);
       }
       // If final is active - Update Final objects
       if (EndGameUIManager.FinalStatus())
       {
           EndGameUIManager.Instance.Update(gameTime);
           returnButton.Update();
           replayButton.Update();
       }
        // Additional ckeck for game's end
       if (GameManager.gameIsOver)
       {
           _timer.SetTimerToZero();
           GeneralUIManager.ShowFinalUI();
           GameManager.gameIsOver = false; 
       }
       
       
       if (currentKeyboard.IsKeyDown(Keys.Escape) && previousKeyboard.IsKeyUp(Keys.Escape))
       {
           GameManager.EscapeMethod();    
       }
       previousKeyboard = currentKeyboard;
       base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkRed);
        _spriteBatch.Begin(SpriteSortMode.FrontToBack);

        SceneManager.Instance.Draw(_spriteBatch);
        MainMenuUIManager.Instance.Draw(_spriteBatch);
        
        // If game is active - Draw Ingame objects
        if (GeneralUIManager.isGameUIActive)
        {
            InGameUIManager.Instance.Draw(_spriteBatch);
        }
        
        // If final is active - Draw Final objects
        if (EndGameUIManager.FinalStatus())
        {
            EndGameUIManager.Instance.Draw(_spriteBatch);
        }
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}