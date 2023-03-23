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


    // styles
    public static StyleManager DefaultStyle { get; private set; }
    public static StyleManager WhiteBackgroundBlackText { get; private set; }
    // ----


    // colors
    public static Color WhiteTextColor { get; } = new Color(245, 231, 222);
    public static Color WhiteColor { get; } = new Color(218, 189, 171);
    public static Color DarkColor { get; } = new Color(52, 48, 45);
    public static Color GreyColor { get; } = new Color(107, 106, 101);
    public static Color DarkBlueColor { get; } = new Color(57, 92, 120);
    public static Color DarkRedColor { get; } = new Color(157, 49, 47);
    public static Color LightBlueColor { get; } = new Color(91, 122, 140);
    public static Color LightRedColor { get; } = new Color(189, 59, 59);
    public static Color TransparentColor { get; } = new Color(0, 0, 0, 0);
    // ----

    static StyleManager()
    {
        DefaultStyle = new StyleManager
        {
            BackgroundColor = DarkColor,
            TextColor = WhiteTextColor,
            OutlineColor = TransparentColor,
            OutlineThickness = 2,
            UIFont = ResourceLoader.FetchFont("default"),
            BaseTextSize = GUIText.TextSize.Medium
        };

        WhiteBackgroundBlackText = new StyleManager
        {
            BackgroundColor = WhiteColor,
            TextColor = DarkColor,
            OutlineThickness = 0,
            UIFont = ResourceLoader.FetchFont("default"),
            BaseTextSize = GUIText.TextSize.HeadingMedium
        };

    }
}