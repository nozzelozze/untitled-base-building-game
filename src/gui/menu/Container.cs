using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

public class Container : GUIElement
{

    private List<GUIElement> ChildElements { get; set; }

    public Container(GUIElementConfig config)
    : base(config)
    {
        ChildElements = new List<GUIElement>();
    }

    public void AddElement(GUIElement element)
    {
        ChildElements.Add(element);
    }

    public void ClearElements() { ChildElements.Clear(); }

    public void RemoveElement(GUIElement element)
    {
        ChildElements.Remove(element);
    }

    public override void Update()
    {
        base.Update();
        Vector2f currentPosition = Position;
        foreach (GUIElement childElement in ChildElements)
        {
            childElement.ElementPosition = currentPosition;
            currentPosition += new Vector2f(0, childElement.BaseRect.Size.Y);
        }
    }
}