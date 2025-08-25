using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace TopDownFinalPR;

public static class EnemyManager
{
    private static List<Vector2> spawnPoints = new List<Vector2>();
    private static Random random = new Random();
    private static List<Enemy> enemies = new List<Enemy>();

    private static Vector2 previousSpot;
    
    // for checking game final
    public static bool IsGameOver { get; private set; } = false;

    // set spawn points
    public static void SetSpawnPoints()
    {
        spawnPoints = new List<Vector2>
        {
            new Vector2(960,1000), // down 90 degrees
            new Vector2(960,80), // top 90 degrees
            new Vector2(80,540), // left 90 degrees
            new Vector2(1840,540), // right 90 degrees
            new Vector2(80,80), // left-top 30 degrees
            new Vector2(1840,80), // right-top 30 degrees
            new Vector2(80,1000), // left-down 30 degrees
            new Vector2(1840,1000) // right-down 30 degrees
        };
    }
    // put enemy on list
    public static void RegisterEnemy(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }
    // Remove enemy from list
    public static void UnregisterEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
    }

    // Destroy all enemies
    public static void KillAllEnemies()
    {
        var enemiesToKill = new List<Enemy>(enemies);
        foreach (Enemy enemy in enemiesToKill)
        {
            UnregisterEnemy(enemy);
            SceneManager.Remove(enemy);
        }
    }

    // Get random spawn point that is now previous point
    public static Vector2 GetSpawnPoint()
    {
        if (spawnPoints.Count == 0)
            return Vector2.Zero;

        Vector2 selectedPoint;
        do
        {
            int index = random.Next(spawnPoints.Count);
            selectedPoint = spawnPoints[index];
        } while (selectedPoint == previousSpot && spawnPoints.Count > 1);

        previousSpot = selectedPoint;
        return selectedPoint;
    }

    // Generic spawn method
    public static T SpawnEnemy<T>() where T : Enemy, new()
    {
        Vector2 spawnPoint = GetSpawnPoint();
        T enemy = new T
        {
            position = spawnPoint
        };
        enemy.SyncNow();
        SceneManager.Add(enemy);
        RegisterEnemy(enemy);
        return enemy;
    }

    // return all enemies in scene
    public static List<Enemy> GetAllEnemies()
    {
        return new List<Enemy>(enemies);
    }
    
}