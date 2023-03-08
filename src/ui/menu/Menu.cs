using System;
using SFML.Graphics;
using SFML.System;

public class Menu : Container
{
    RectangleShape barRect;
    Text titleText;
    public IconButton closeButton;

    public static Vector2f infoMenuPosition = new Vector2f(1500, 550);

    public const float barRectSizeY = 32;

    public Menu(string title, Vector2f position, List<GUIActor> ? items = null, AlignType alignType = AlignType.Center) : base(position, alignType)
    {
        if (items != null)
        {
            foreach(GUIActor item in items)
            {
                addRow(new List<GUIActor>{item});
            }
        }
        barRect = new RectangleShape(new Vector2f(defaultSizeX, barRectSizeY));
        barRect.OutlineThickness = GUIActor.outlineThickness;
        barRect.OutlineColor = GUIActor.outlineColor;
        barRect.FillColor = GUIColor.blueColor;
        barRect.Position = Position;
        titleText = new Text(title, ResourceLoader.fetchFont("default"));
        titleText.CharacterSize = GUIActor.getCharacterSize(GUIActor.characterSize.HeadingSmall);
        titleText.Position = Position + new Vector2f(5, 0);
        closeButton = new IconButton(
            ResourceLoader.fetchTexture(ResourceLoader.TextureType.CloseIcon), 
            closeWindow,
            new Vector2f(Position.X+(float)defaultSizeX-barRectSizeY, Position.Y)
        );
    }

    public void closeWindow()
    {

    }

    public void changeTitle(string newTitle)
    {
        titleText.DisplayedString = newTitle;
    }

    public void addItem(GUIActor item)
    {
        addRow(new List<GUIActor>{item});
    }

    public override void render()
    {
        base.render();
        barRect.Position = Position;
        titleText.Position = Position + new Vector2f(5, 0);
        closeButton.Position = new Vector2f(Position.X+(float)getSize().X-32f, Position.Y);
        barRect.Size = new Vector2f(baseRect.Size.X, barRectSizeY);
        RenderQueue.queueGUI(barRect);
        RenderQueue.queueGUI(titleText);
        closeButton.render(); 
    }
}