using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

public class InGameUIManager: IUpdateable, IDrawable
{
    private static InGameUIManager instance;
    
    private static List<IUIElements> gameUIElements = new List<IUIElements>(); //active list
    private static List<IUIElements> gameUISave = new List<IUIElements>();// list for restoring

    // Singleton access to the InGameUIManager
    public static InGameUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InGameUIManager();

            return instance;
        }
    }
    
    // Adds a UI element to the InGame lists
    public static void AddObjToGameUIList(IUIElements obj)
    {
        gameUIElements.Add(obj);
        gameUISave.Add(obj);
    }
    
    // Clear all elements
    public static void ClearGameUI()
    {
        gameUIElements.Clear();
    }
    
    
    // Restores UI elements from the saved list to the active UI list
    public static void RestoreGameUI()
    {
        gameUIElements.Clear();
        foreach (IUIElements ui in gameUISave)
        {
            gameUIElements.Add(ui);
        }
    }
    
    
    public void Update(GameTime gameTime)
    {
        for (int i = 0; i < gameUIElements.Count; i++)
        {
            gameUIElements[i].Update(gameTime);
        }
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        foreach (IUIElements drawable in gameUIElements)
        {
            drawable.Draw(_spriteBatch);
        }
    }
}