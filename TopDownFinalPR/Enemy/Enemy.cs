using Microsoft.Xna.Framework;

namespace TopDownFinalPR;

public abstract class Enemy : Animation
{
    
    // Reference to the player in the scene
    public Player player = null;
    // Collider used for hit detection
    public Collider collider;

    // Enemy stats
    protected float hp;
    protected float damage;
    protected float speed;

    // Animation and state flags
    protected bool isDead = false;
    protected bool isAttacking = false;
    protected bool attackAnimationPlayed = false;

    // Constructor
    protected Enemy(string animationName) : base(animationName)
    {
    }

    // Finds the player object in the scene and stores reference
    protected void FindPlayer()
    {
        var sceneObjects = SceneManager.GetAllObjects();
        foreach (var obj in sceneObjects)
        {
            if (obj is Player playerObj)
            {
                this.player = playerObj;
                break;
            }
        }
    }

    // Flips enemy sprite to face the player
    protected void FacePlayer()
    {
        if (player == null) return;
        bool lookLeft = player.position.X < position.X;
        Flip(lookLeft);
    }

    // Applies damage to the enemy and checks for death
    public virtual void TakeDamage(float d)
    {
        hp -= d;
        CheckHealth();
    }

    // Checks if HP dropped to 0 and triggers death
    protected virtual void CheckHealth()
    {
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }
    
    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;
        StopAnimation();
        SetAnim("", false, 10); 
    }

    protected virtual void AttackPlayer()
    {
        if (player != null && player.IsAlive())
        {
            if (isAttacking) return;

            isAttacking = true;
            StopAnimation();
            SetAnim("", false, 10); 
        }
    }

    // Checks if enemy is hit by an arrow and takes lethal damage
    protected virtual void CheckArrowCollision()
    {
        var sceneObjects = SceneManager.GetAllObjects();
        foreach (var obj in sceneObjects)
        {
            if (obj is Arrow arrow)
            {
                float distance = Vector2.Distance(position, arrow.position);
                if (distance < 50f)
                {
                    TakeDamage(1000f);
                    break;
                }
            }
        }
    }
}
