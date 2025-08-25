using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

public static class GeneralUIManager
{
    public static bool isGameUIActive = false;
    
    // Show Main menu
    public static void ShowMainMenu()
    {
        CloseAllUI();
        MainMenuUIManager.RestoreMainMenu();
        MainMenuUIManager.ActivateMenu();
    }

    // Hide main Menu
    private static void HideMainMenu()
    {
        MainMenuUIManager.ClearMainMenu();
    }

    // Show InGame Panel
    public static void ShowGameUI()
    {
        CloseAllUI();
        InGameUIManager.RestoreGameUI();
        GameManager.StartGame();
    }

    // Hide InGame elements
    public static void HideGameUI()
    {
        InGameUIManager.ClearGameUI();
    }
    
    // Show Final Panel
    public static void ShowFinalUI()
    {
        CloseAllUI();
        EndGameUIManager.RestoreEndGameUI();
        EndGameUIManager.ActivateFinal();
    }

    // Hide only Final panel
    private static void HideFinalUI()
    {
        EndGameUIManager.ClearEndGameUI();
        EndGameUIManager.HideFinal();
        EndGameUIManager.ResetFinalFlag();
    }

    // Hide all Panels
    private static void CloseAllUI()
    {
        HideMainMenu();
        HideGameUI();
        HideFinalUI();
    }
    
}