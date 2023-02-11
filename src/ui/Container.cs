using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Container : GUIActor
{

    List<GUIActor> items = new List<GUIActor>();
    RectangleShape barRect;

    Text titleText;

    public IconButton closeButton;

    /* Sizex, sizey AND closebutton should be in a infomenu class?  */

    public const int sizeX = 350;
    public const int sizeY = 450;
    
    public Container(string title, Vector2f position) : base(new Vector2f(sizeX, sizeY), position)
    {
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
            new Vector2f(Position.X+(float)sizeX-32f, Position.Y),
            closeWindow
        );
    }

    public void closeWindow()
    {

    }

    public override void render()
    {
        base.render();
        RenderQueue.queueGUI(barRect);
        RenderQueue.queueGUI(titleText);
        closeButton.render();
    }

}