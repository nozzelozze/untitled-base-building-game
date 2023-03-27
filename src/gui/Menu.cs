using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;


public class Menu : GUIElement
{
    
    private GUIText Title;

    protected RectangleShape BarRect;

    private const int BarRectHeight = 20;

    public Menu(string title, Dictionary<string, List<GUIElement>> tabs, Vector2f startPosition, StyleManager ? style = null)
    : base(startPosition, style)
    {
        //Title = new GUIText(title, startPosition, )
        BarRect = new RectangleShape(new Vector2f(100, BarRectHeight));

    }

    public override void Update()
    {
        base.Update();

        BarRect.Position = Position;
        RenderQueue.QueueGUI(BarRect);

    }

}