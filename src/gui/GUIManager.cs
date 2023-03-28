using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;


public class GUIManager
{
    private static List<GUIElement> GUIElements = new List<GUIElement>();
    public static List<InteractiveGUIElement> InteractiveGUIElements = new List<InteractiveGUIElement>();

    public GUIManager()
    {

    }

    public static void AddGUIElement(GUIElement element)
    {
        GUIElements.Add(element);
        if (element is InteractiveGUIElement interactiveElement)
        {
            InteractiveGUIElements.Add(interactiveElement);
        }
    }

    public static void RemoveGUIElement(GUIElement element)
    {
        GUIElements.Remove(element);
        if (element is InteractiveGUIElement interactiveElement)
        {
            InteractiveGUIElements.Remove(interactiveElement);
        }
        IReadOnlyList<GUIElement> childElements = element.GetChildGUIElements();
        if (childElements.Count > 0)
        {
            foreach (GUIElement child in childElements)
            {
                RemoveGUIElement(child);
            }
        }
    }

    public void Update()
    {
        foreach (var element in GUIElements)
        {
            element.Update();
        }

        foreach (var element in InteractiveGUIElements)
        {
            Vector2i mousePosition = PlayerMouse.GetPosition();
            if (element.BaseRect.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
            {
                if (!element.IsMouseOver)
                {
                    element.OnMouseEnter();
                    element.IsMouseOver = true;
                }
            }
            else if (element.IsMouseOver)
            {
                element.OnMouseLeave();
                element.IsMouseOver = false;
            }
        }
    }
}
