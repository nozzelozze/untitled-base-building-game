using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class IconOnlyButton : Button
{
    private Sprite Icon { get; set; }

    public IconOnlyButton(Vector2f position, Action onClick, Texture iconTexture)
    : base(position, onClick)
    {
        Icon = new Sprite(iconTexture);
        Icon.Position = Position;
        BaseRect.Size = (Vector2f)Icon.Texture.Size;
    }

    protected override void UpdateAppearance()
    {
        base.UpdateAppearance();
        switch (State)
        {
            case ButtonState.Hovered:
                Icon.Color = StyleManager.GreyColor;
                break;
            case ButtonState.Normal:
                Icon.Color = new Color(255,255,255);
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        RenderQueue.QueueGUI(Icon);
    }
}