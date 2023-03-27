using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

public class Menu : GUIElement
{
    
    private GUIText Title;

    protected RectangleShape BarRect;

    private const int BarRectHeight = 20;

    public Menu(GUIElementConfig config, string title, Dictionary<string, List<GUIElement>> tabs)
    : base(config)
    {
        Title = new GUIText
        (
            title,
            new GUIElementConfig
                {
                    StartPosition = new Vector2f(2, 0),
                    RelativeTo = Position
                }
        );
        BarRect = new RectangleShape(new Vector2f(100, BarRectHeight));
        BarRect.FillColor = StyleManager.MenuBarColor;
    }

    public override void Update()
    {
        base.Update();

        BarRect.Position = Position;
        RenderQueue.QueueGUI(BarRect);

    }

}