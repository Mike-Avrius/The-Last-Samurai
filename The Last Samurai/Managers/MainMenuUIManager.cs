using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

public class MainMenuUIManager : IUpdateable, IDrawable
{
    // Singleton instance
    private static MainMenuUIManager instance;
    
    // UI element collections
    private static List<IUIElements> mainMenuElements = new List<IUIElements>();    
    private static List<IUIElements> mainMenuSave = new List<IUIElements>();        
    
    // Menu state
    private static bool menuIsAcrive;                                               
    
    
    // Singleton instance accessor - creates instance if it doesn't exist
    public static MainMenuUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new MainMenuUIManager();
            
            return instance;
        }
    }
    
    /// Adds a UI element to both the active menu list and the backup list
    public static void AddObjToMainMenuList(IUIElements obj)
    {
        mainMenuElements.Add(obj);
        mainMenuSave.Add(obj);
    }
    
    
    // Clears the active menu elements and deactivates the menu
    public static void ClearMainMenu()
    {
        mainMenuElements.Clear();
        menuIsAcrive = false;
    }

    
    // Returns the current status of the main menu
    public static bool MenuStatus()
    {
        return menuIsAcrive;
    }

    // Activates the main menu
    public static void ActivateMenu()
    {
        menuIsAcrive = true;
    }
    
    
    /// Restores the main menu by copying all saved elements back to the active list
    public static void RestoreMainMenu()
    {
        mainMenuElements.Clear();
        foreach (IUIElements ui in mainMenuSave)
        {
            mainMenuElements.Add(ui);
        }

        menuIsAcrive = true;
    }
    
    
    // Updates all main menu UI elements each frame
    public void Update(GameTime gameTime)
    {
        for (int i = 0; i < mainMenuElements.Count; i++)
        {
            mainMenuElements[i].Update(gameTime);
        }
    }

    
    // Renders all main menu UI elements to the screen
    public void Draw(SpriteBatch _spriteBatch)
    {
        foreach (IUIElements drawable in mainMenuElements)
        {
            drawable.Draw(_spriteBatch);
        }
    }
}