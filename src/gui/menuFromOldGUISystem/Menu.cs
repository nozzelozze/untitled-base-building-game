using System;
using SFML.Graphics;
using SFML.System;

/* public class Menu : Container
{
    RectangleShape BarRect;
    Text TitleText;
    public IconButton CloseButton;

    public static Vector2f InfoMenuPosition = new Vector2f(1500, 550);

    public const float BarRectSizeY = 32;

    public Menu(string title, Vector2f position, List<GUIActor>? items = null, AlignType alignType = AlignType.Center) : base(position, alignType)
    {
        if (items != null)
        {
            foreach(GUIActor item in items)
            {
                AddRow(new List<GUIActor>{item});
            }
        }
        BarRect = new RectangleShape(new Vector2f(DefaultSizeX, BarRectSizeY));
        BarRect.OutlineThickness = GUIActor.OutlineThickness;
        BarRect.OutlineColor = GUIActor.OutlineColor;
        BarRect.FillColor = GUIColor.BlueColor;
        BarRect.Position = Position;
        TitleText = new Text(title, ResourceLoader.FetchFont("default"));
        TitleText.CharacterSize = GUIActor.GetCharacterSize(GUIActor.CharacterSize.HeadingSmall);
        TitleText.Position = Position + new Vector2f(5, 0);
        CloseButton = new IconButton(
            ResourceLoader.FetchTexture(ResourceLoader.TextureType.CloseIcon), 
            CloseWindow,
            new Vector2f(Position.X+(float)DefaultSizeX-BarRectSizeY, Position.Y)
        );
    }

    public void CloseWindow()
    {

    }

    public void ChangeTitle(string newTitle)
    {
        TitleText.DisplayedString = newTitle;
    }

    public void AddItem(GUIActor item)
    {
        AddRow(new List<GUIActor>{item});
    }

    public override void Render()
    {
        base.Render();
        BarRect.Position = Position;
        TitleText.Position = Position + new Vector2f(5, 0);
        CloseButton.Position = new Vector2f(Position.X+(float)GetSize().X-32f, Position.Y);
        BarRect.Size = new Vector2f(BaseRect.Size.X, BarRectSizeY);
        RenderQueue.QueueGUI(BarRect);
        RenderQueue.QueueGUI(TitleText);
        CloseButton.Render(); 
    }
}
 */