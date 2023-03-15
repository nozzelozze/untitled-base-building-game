using System;
using SFML.Graphics;

public static class RenderQueue
{
    
    public static List<Drawable> RenderList = new List<Drawable>();
    public static List<Drawable> RenderGUIList = new List<Drawable>();

    public static void Queue(Drawable drawableToQueue)
    {
        RenderList.Add(drawableToQueue);
    }
    public static void QueueGUI(Drawable drawableToQueue)
    {
        RenderGUIList.Add(drawableToQueue);
    }

    public static void Render(RenderWindow renderWindow, View uiView)
    {
        foreach (Drawable drawable in RenderList)
        {
            renderWindow.Draw(drawable);
        }
        RenderList.Clear();
        renderWindow.SetView(uiView);
        foreach (Drawable GUIdrawable in RenderGUIList)
        {
            renderWindow.Draw(GUIdrawable);
        }
        RenderGUIList.Clear();
    }

}
