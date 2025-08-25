using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownFinalPR;

public class InGameUIElements : Sprite, IUIElements 
{
    public InGameUIElements(string textureName) : base(textureName)
    {
        layerIndex = 0.9f;
    }

    public void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public new void Draw(SpriteBatch _spriteBatch)
    {
        base.Draw(_spriteBatch);
    }
}