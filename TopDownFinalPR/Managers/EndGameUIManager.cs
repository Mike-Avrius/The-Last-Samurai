using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

public class EndGameUIManager: IUpdateable, IDrawable
{
    private static EndGameUIManager instance;
        
    private static List<IUIElements> endGameUIElements = new List<IUIElements>(); //active list
    private static List<IUIElements> endGameUISave = new List<IUIElements>(); // list for restoring
    
    private static bool finalIsActive;
    
    // Singleton access to the EndGameUIManager
    public static EndGameUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EndGameUIManager();
    
            return instance;
        }
    }
    
    // Resets the final flag to false
    public static void ResetFinalFlag()
    {
        finalIsActive = false;
    }
    
    // Sets the final flag to true, indicating the final screen is active
    public static void ActivateFinal()
    {
        finalIsActive = true;
    }
    
    // Returns the current state of the final screen flag
    public static bool FinalStatus()
    {
        return finalIsActive;
    }
    
    // Adds a UI element to the final lists
    public static void AddObjToEndGameUIList(IUIElements obj)
    {
        endGameUIElements.Add(obj);
        endGameUISave.Add(obj);
    }
        
    // Clears the final screen UI and resets the flag
    public static void ClearEndGameUI()
    {
        ResetFinalFlag();
        endGameUIElements.Clear();
    }
        
    // Restores UI elements from the saved list to the active UI list
    public static void RestoreEndGameUI()
    {
        endGameUIElements.Clear();
        foreach (IUIElements ui in endGameUISave)
        {
            endGameUIElements.Add(ui);
        }  
    }
    
    // Hides the final screen and clears all current UI elements
    public static void HideFinal()
    {
        finalIsActive = false; 
        endGameUIElements.Clear();
    }
        
        
    public void Update(GameTime gameTime)
    {
        for (int i = 0; i < endGameUIElements.Count; i++)
        {
            endGameUIElements[i].Update(gameTime);
        }
    }
    
    public void Draw(SpriteBatch _spriteBatch)
    {
        foreach (IUIElements drawable in endGameUIElements)
        {
            drawable.Draw(_spriteBatch);
        }
    }
}