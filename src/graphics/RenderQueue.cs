using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public static class RenderQueue
{
    
    public static List<Drawable> renderList = new List<Drawable>();
    public static List<Drawable> renderGUIList = new List<Drawable>();

    public static void queue(Drawable drawableToQueue)
    {
        renderList.Add(drawableToQueue);
    }
    public static void queueGUI(Drawable drawableToQueue)
    {
        renderGUIList.Add(drawableToQueue);
    }

    public static void render(RenderWindow renderWindow, View uiView)
    {
        foreach (Drawable drawable in renderList)
        {
            renderWindow.Draw(drawable);
        }
        renderList.Clear();
        renderWindow.SetView(uiView);
        foreach (Drawable GUIdrawable in renderGUIList)
        {
            renderWindow.Draw(GUIdrawable);
        }
        renderGUIList.Clear();
    }

}