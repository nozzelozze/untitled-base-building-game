using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Container : GUIActor
{

    List<GUIActor> items = new List<GUIActor>();
    RectangleShape barRect;

    Text titleText;
    
    public Container(string title) : base(new Vector2f(500, 500), new Vector2f(500, 500))
    {
        barRect = new RectangleShape(new Vector2f(500, 35));
        barRect.OutlineThickness = GUIActor.outlineThickness;
        barRect.OutlineColor = GUIActor.outlineColor;
        barRect.FillColor = GUIColor.blueColor;
        barRect.Position = Position;
        titleText = new Text(title, ResourceLoader.fetchFont("default"));
        titleText.CharacterSize = GUIActor.getCharacterSize(GUIActor.characterSize.HeadingSmall);
        titleText.Position = Position + new Vector2f(5, 0);
    }

    public override void render()
    {
        base.render();
        RenderQueue.queueGUI(barRect);
        RenderQueue.queueGUI(titleText);
    }

}