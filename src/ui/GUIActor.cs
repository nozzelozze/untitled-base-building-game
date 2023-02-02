using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class GUIActor : Transformable
{


    FloatRect collisionRect;
    RectangleShape drawRect = new RectangleShape();
    public Vector2f size;

    public GUIActor(Vector2f size, Vector2f position)
    {
        collisionRect.Width = size.X;
        collisionRect.Height = size.Y;
        drawRect.Size = size;
        Position = position;
    }

    public void render()
    {
        drawRect.Position = Position;
        RenderQueue.queueGUI(drawRect);
        if (Input.events.Contains(Mouse.Button.Left) && Player.currentState == PlayerState.IdleState.IdleInstance)
        {
            Vector2i mousePosition = PlayerMouse.getPosition();
            if (collisionRect.Contains(mousePosition.X, mousePosition.Y))
            {
                Log.Message("Clicked an button!");
            }
        }
    }

}