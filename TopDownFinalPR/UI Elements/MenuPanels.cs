namespace TopDownFinalPR;

public class MenuPanels : Sprite, IUIElements
{
    // Construst for Menu background panels
    public MenuPanels(string textureName) : base(textureName)
    {
        position = Game1.ScreenCenter;
        layerIndex = 0f;
    }
}