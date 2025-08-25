using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

public class Arrow : Sprite
{
    private Vector2 direction;
    private float speed;
    private float lifetime;
    private float currentLifetime;
    
    private Collider collider;

    // Static list to track all active arrows
    private static List<Arrow> activeArrows = new List<Arrow>();

    // Constructor that initializes arrow's position, direction, and collider
    public Arrow(Vector2 startPosition, Vector2 targetPosition) : base("Arrow")
    {
        position = startPosition;
        scale = new Vector2(2.0f, 2.0f);
        layerIndex = 0.8f;

        speed = 600f;
        lifetime = 5f;
        currentLifetime = 0f;

        direction = targetPosition - startPosition;

        if (direction.Length() > 0.1f)
        {
            direction.Normalize();
        }
        else
        {
            Console.WriteLine("WARNING: Arrow direction is too small! Using default direction.");
            direction = new Vector2(1, 0);
        }

        rotation = (float)Math.Atan2(direction.Y, direction.X);

        collider = SceneManager.Create<Collider>();
        collider.isTrigger = true;
        collider.visible = false;
        collider.position = position;
        collider.rect = new Rectangle((int)position.X - 25, (int)position.Y - 25, 50, 50);

        SceneManager.Add<Arrow>(this);
        activeArrows.Add(this);
    }

    // Update arrow movement, lifetime, and collision
    public override void Update(GameTime gameTime)
    {
        Vector2 oldPosition = position;

        position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        currentLifetime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (currentLifetime >= lifetime)
        {
            SceneManager.Remove(this);
            activeArrows.Remove(this);
            return;
        }

        if (collider != null)
        {
            collider.position = position;
            collider.rect = new Rectangle((int)position.X - 25, (int)position.Y - 25, 50, 50);
        }
        else
        {
            Console.WriteLine("ERROR: Arrow collider is null!");
        }

        CheckEnemyCollision();
    }

    // Check collision with enemies
    private void CheckEnemyCollision()
    {
        if (EnemyManager.IsGameOver) return;

        var enemies = EnemyManager.GetAllEnemies();
        if (enemies.Count == 0) return;

        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.collider != null)
            {
                if (collider.Intersects(enemy.collider))
                {
                    AudioManager.PlaySoundEffect("hit_sound");
                    enemy.TakeDamage(1000f);
                    Destroy();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"ERROR: Enemy or its collider is null: enemy={enemy}, collider={enemy?.collider}");
            }
        }
    }

    // Destroy this arrow and its collider
    private void Destroy()
    {
        if (collider != null)
        {
            SceneManager.Remove(collider);
        }

        SceneManager.Remove(this);
        activeArrows.Remove(this);
    }

    // Static method to destroy all arrows currently in the scene
    public static void DestroyAllArrows()
    {
        foreach (var arrow in activeArrows)
        {
            if (arrow.collider != null)
                SceneManager.Remove(arrow.collider);

            SceneManager.Remove(arrow);
        }

        activeArrows.Clear();
    }
}
