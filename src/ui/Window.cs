using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Window : Transformable
{

    public RectangleShape baseRect;
    public RectangleShape barRect;
    public Text title;

    public Window(Vector2f size, string titleName)
    {
        baseRect = new RectangleShape(size);
        baseRect.FillColor = Color.Blue;
        baseRect.OutlineColor = Color.Black;
        baseRect.OutlineThickness = 5;
        barRect = new RectangleShape(new Vector2f(size.X, 20f));
        barRect.FillColor = Color.Cyan;
        barRect.OutlineThickness = 5;
        title = new Text(titleName, );
        Position = new Vector2f(500, 500);
    }

    public void render()
    {
        baseRect.Position = Position;
        RenderQueue.queue(baseRect);
    }
}