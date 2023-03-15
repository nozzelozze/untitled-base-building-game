using System;
using SFML.Graphics;
using SFML.System;

public abstract class GUIElement : Transformable
{

    protected FloatRect CollisionRect;
    protected RectangleShape BaseRect;
    protected bool IsTransparent;

    protected StyleManager Style;

    public GUIElement(StyleManager? style = null)
    {
        BaseRect = new RectangleShape();
        this.Style = style ?? StyleManager.DefaultStyle;
    }


}
