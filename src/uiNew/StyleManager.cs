using System;
using SFML.Graphics;
using SFML.System;


public class StyleManager
{
    public Color BackgroundColor { get; set; }
    public Color TextColor { get; set; }
    public Color OutlineColor { get; set; }
    public int OutlineThickness { get; set; }
    public Font UIFont { get; set; }
    public GUIText.TextSize BaseTextSize { get; set; }

    public static StyleManager DefaultStyle { get; private set; }

    static StyleManager()
    {
        DefaultStyle = new StyleManager();
        DefaultStyle.BackgroundColor = Color.Blue;
        DefaultStyle.TextColor = Color.White;
        DefaultStyle.OutlineColor = Color.Red;
        DefaultStyle.OutlineThickness = 2;
        DefaultStyle.UIFont = ResourceLoader.FetchFont("default");
        DefaultStyle.BaseTextSize = GUIText.TextSize.Medium;
    }
}