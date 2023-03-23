using System;
using SFML.Graphics;
using SFML.System;

public class GUIText : GUIElement
{

    public enum TextSize
    {
        Small,
        Medium,
        Large,
        HeadingSmall,
        HeadingMedium,
        HeadingLarge
    }

    Text Text;
    Func<object>? TickVar;
    string DisplayString;

    public GUIText(string text, Func<object>? tickVar = null, StyleManager ? style = null)
    : base(new Vector2f(), style)
    {
        DisplayString = text;
        Text = new Text(text, Style.UIFont);
        Text.CharacterSize = GetCharacterSize(Style.BaseTextSize);
        Text.FillColor = Style.TextColor;
        TickVar = tickVar;
        if (!text.Contains("%v") && tickVar != null)
        {
            Log.Error($"'{text}' does not include '%v' but a tick variable is given.");
        }

        FloatRect globalBounds = Text.GetGlobalBounds();
        BaseRect.Size = new Vector2f(
            globalBounds.Width,
            globalBounds.Height
        );
    }

    private uint GetCharacterSize(TextSize textSize)
    {
        switch (textSize)
        {
            case TextSize.Small:
                return 12;
            case TextSize.Medium:
                return 16;
            case TextSize.Large:
                return 20;
            case TextSize.HeadingSmall:
                return 25;
            case TextSize.HeadingMedium:
                return 30;
            case TextSize.HeadingLarge:
                return 35;
            default:
                throw new ArgumentOutOfRangeException(nameof(textSize), $"Unsupported text size: {textSize}");
        }
    }

    public override void Update()
    {
        base.Update();
        Text.Position = Position;
        if (TickVar != null)
        {
            Text.DisplayedString = DisplayString.Replace("%v", TickVar().ToString());
        }
        RenderQueue.QueueGUI(Text);
    }
}
