using Microsoft.Xna.Framework;

namespace TopDownFinalPR;

public class Animation : Sprite
{
    private SpriteSheet spriteSheet;
    
    private int columns;
    private int rows;

    private int index_x = 0;
    private int index_y = 0;
    
    private double frameTimer = 0;
    private bool animating = false;
    private int fps;
    private bool inLoop;
    
    private string currentAnim = null;
    
    protected Animation(string animationName) : base(animationName)
    {
        ChangeAnimation(animationName);
    }
    
    private void ChangeAnimation(string animationName)
    {
        spriteSheet = new SpriteSheet(SpriteManager.GetSprite(animationName));

        texture = spriteSheet.SpriteSheetInfo.texture; 
        columns = spriteSheet.SpriteSheetInfo.columns;
        rows = spriteSheet.SpriteSheetInfo.rows;
    }
    
    protected override void SetOrigin(OriginPosition originPosition)
    {
        switch (originPosition)
        {
            case OriginPosition.TopLeft:
                origin = Vector2.Zero;
                break;
                
            case OriginPosition.Center:
                origin = new Vector2(sourceRectangle.Value.Size.X * 0.5f, sourceRectangle.Value.Size.Y * 0.5f);
                break;
        }
    }
    
    private void PlayAnimation(bool inLoop = true, int fps = 60)
    {
        this.fps = fps;
        this.inLoop = inLoop;
        
        ResetAnimation();
        animating = true;
    }

    protected bool IsAnimating()
    {
        return animating;
    }
    
    public void PauseAnimation()
    {
        animating = false;
    }
    
    public void StopAnimation()
    {
        PauseAnimation();
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        frameTimer = 0;
        index_x = 0;
        index_y = 0;
    }

    bool ShouldGetNextFrame(GameTime gameTime)
    {
        frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
        
        if (frameTimer > (1.0 / fps))
            return true;
        
        return false;
    }

    public void MoveNextFrame()
    {
        frameTimer = 0;

        if (inLoop)
        {
            index_x++;

            if (index_x == columns)
            {
                index_y++;
                index_y %= rows;
            }

            index_x %= columns;
        }
        else
        {
            if (index_x + 1 < columns)
            {
                index_x++;
            }
            else if (index_y + 1 < rows)
            {
                index_y++;
                index_x = 0;
            }
            else
            {
                animating = false;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (animating)
        {
            if (ShouldGetNextFrame(gameTime))
                MoveNextFrame();
        }

        sourceRectangle = spriteSheet[index_x, index_y];
        
        Rectangle r = sourceRectangle ?? new Rectangle(0, 0, 0, 0);;
        rect = GetDestRectangle(r);
        
        base.Update(gameTime);
    }
    
    public void SyncNow()
    {
        if (sourceRectangle == null)
            sourceRectangle = spriteSheet[0, 0];

        // Recalculate the destination rectangle based on the current frame and position
        rect = GetDestRectangle(sourceRectangle.Value);

        // Recalculate the origin based on the current frame size
        SetOrigin(originPosition);
    }
    
    // Set and play animation
    public void SetAnim(string name, bool loop = true, int fps = 10)
    {
        // If the requested animation is already playing, skip resetting it
        if (currentAnim == name && IsAnimating()) return;

        ChangeAnimation(name);
        PlayAnimation(loop, fps);
        currentAnim = name;
    }
}