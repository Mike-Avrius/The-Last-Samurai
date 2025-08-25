using System;

namespace TopDownFinalPR;

public static class GameManager
{
    public static bool gameIsOver = false;
    public static int dificulty = 2;
    
    // Reset all previous flags, timer, and prepare for game start
    public static void StartGame()
    {
        if (Game1.CurrentTimer != null)
        {
           Game1.CurrentTimer.ResetTimer();
        }

        EndGameUIManager.ResetFinalFlag();
        InGameUIManager.RestoreGameUI();
        PlayerManager.ChangeCreatePlayerFlag();
        GeneralUIManager.isGameUIActive = true;
    }
    
    // Destroy all game objects and set final Panel flag
    public static void GameIsOver()
    { 
       EnemyManager.KillAllEnemies();
       PlayerManager.KillPlayer();
       Arrow.DestroyAllArrows();
       EndGameUIManager.ActivateFinal();
       gameIsOver = true;
    }
    
    // Game is over if Escape is pressed
    public static void EscapeMethod()
    { 
        EnemyManager.KillAllEnemies();
        PlayerManager.KillPlayer();
        Arrow.DestroyAllArrows();
        GeneralUIManager.HideGameUI();
        GeneralUIManager.ShowMainMenu();
    }
    
    public static void SetDificulty(int d)
    {
        dificulty = d;
    }
    
}
