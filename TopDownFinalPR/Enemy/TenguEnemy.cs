using Microsoft.Xna.Framework;

namespace TopDownFinalPR;

public class TenguEnemy : Enemy
{
    private bool deathAnimationPlayed = false;

    // Tengu Constructor
    public TenguEnemy() : base("EnemyTengu_Idle")
    {
        EnemyManager.RegisterEnemy(this);
        speed = 250f;
        scale = new Vector2(1.5f, 1.5f);
        layerIndex = 0.5f;

        collider = SceneManager.Create<Collider>();
        collider.isTrigger = true;
        collider.visible = false;

        hp = 100f;
        damage = 20f;

        FindPlayer();
        SetAnim("EnemyTengu_Walk", true, 10);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        // If dead and death animation finished — remove from scene
        if (isDead)
        {
            if (!deathAnimationPlayed && !IsAnimating())
            {
                deathAnimationPlayed = true;
                EnemyManager.UnregisterEnemy(this);
                SceneManager.Remove(this);
            }
            return;
        }
        // If attacking and attack animation finished — deal damage and die
        if (isAttacking)
        {
            if (!attackAnimationPlayed && !IsAnimating())
            {
                attackAnimationPlayed = true;
                player.TakeDamage((int)damage);
                Die();
            }
            return;
        }

        collider.rect = rect;
        // If player is alive — move toward player and face them
        if (player != null && player.IsAlive())
        {
            Vector2 direction = player.position - position;
            if (direction != Vector2.Zero)
                direction.Normalize();

            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            FacePlayer();

            // Attack if close enough
            float distanceToPlayer = Vector2.Distance(position, player.position);
            if (distanceToPlayer < 50f)
            {
                AttackPlayer();
            }
        }
        else
        {
            Vector2 centerDirection = Game1.ScreenCenter - position;
            if (centerDirection != Vector2.Zero)
                centerDirection.Normalize();

            position += centerDirection * speed * 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        // Notify collision with player if triggered
        if (player != null && player.IsAlive() && player.collider != null && collider.Intersects(player.collider))
        {
            collider.Notify(this);
        }

        CheckArrowCollision();
    }

    // Death logic and animation
    protected override void Die()
    {
        if (isDead) return;
        isDead = true;
        StopAnimation();
        SetAnim("EnemyTengu_Dead", false, 10);
    }

    // Attack and animation
    protected override void AttackPlayer()
    {
        if (player != null && player.IsAlive())
        {
            if (isAttacking) return;
            isAttacking = true;
            StopAnimation();
            SetAnim("EnemyTengu_Attack", false, 10);
        }
    }
}
