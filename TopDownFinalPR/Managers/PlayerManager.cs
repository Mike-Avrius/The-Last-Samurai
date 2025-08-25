using System.Collections.Generic;

namespace TopDownFinalPR;

public static class PlayerManager
{
    // Player creation state
    private static bool playerCreated = false;                                     

    // Active player collection
    public static List<Player> playerS = new List<Player>();                       

    
    /// Creates a new player if one doesn't already exist
    public static Player CreatePlayer()
    {
        if (!playerCreated)
        {
            playerCreated = true; 
            Player player = new Player();
            SceneManager.Add(player);
            playerS.Add(player);
            return player;
        }
        else
        {
            // Kill existing player before creating new one
            KillPlayer();
            return null;
        }
    }
    
    
    // Resets the player creation flag to allow creating a new player
    public static void ChangeCreatePlayerFlag()
    {
        playerCreated = false;
    }

    
    // Removes all active players from the scene and clears the player list
    public static void KillPlayer()
    {
        foreach (Player p in playerS)
        {
            SceneManager.Remove(p);
        }
    }
}