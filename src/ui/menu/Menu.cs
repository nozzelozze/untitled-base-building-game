using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Menu : Container
{
    RectangleShape barRect;
    Text titleText;
    public IconButton closeButton;

    public Menu(string title, Vector2f position, List<GUIActor> ? items = null) : base(position, AlignType.Center)
    {
        if (items != null)
        {
            foreach(GUIActor item in items)
            {
                addRow(new List<GUIActor>{item});
            }
        }
        barRect = new RectangleShape(new Vector2f(sizeX, 32));
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
            new Vector2f(Position.X+(float)sizeX-32f, Position.Y)
        );
    }

    public void closeWindow()
    {

    }

    public void addItem(GUIActor item)
    {
        addRow(new List<GUIActor>{item});
    }

    public override void render()
    {
        base.render();
        RenderQueue.queueGUI(barRect);
        RenderQueue.queueGUI(titleText);
        closeButton.render(); 
    }
}