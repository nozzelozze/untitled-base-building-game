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

    public enum AnchorPoint
    {
        Topleft,
        Center,
        Left
    }
    public AnchorPoint anchorPoint;

    public Text Text { get; private set; }
    Func<object>? TickVar;
    string DisplayString;
    

    public GUIText(string text, GUIElementConfig config, Func<object>? tickVar = null, AnchorPoint anchorPoint = AnchorPoint.Topleft, bool hasBackgroundColor = false)
    : base(config)
    {
        DisplayString = text;
        DisplayString = DisplayString.Replace("%n", Environment.NewLine);
        Text = new Text(DisplayString, Style.UIFont);
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
            globalBounds.Height*1.25f
        );
        this.anchorPoint = anchorPoint;
        SetAnchorPosition();

        if (!hasBackgroundColor)
        {
            Color transparentColor = new Color(BaseRect.FillColor);
            transparentColor.A = 0;
            BaseRect.FillColor = transparentColor;
        }
        BaseRect.OutlineThickness = 0;
    }

    private void CenterText(Text textToCenter, Vector2f centerPosition)
    {
        FloatRect textRect = textToCenter.GetLocalBounds();
        textToCenter.Origin = new Vector2f(
            textRect.Left + textRect.Width/2.0f,
            textRect.Top + textRect.Height/2.0f
        );
        textToCenter.Position = centerPosition;
    }

    public void SetAnchorPosition()
    {
        switch (anchorPoint)
        {
            case AnchorPoint.Topleft:
                Text.Position = Position;
                break;
            case AnchorPoint.Center:
                CenterText(Text, Position);
                break;
            case AnchorPoint.Left:
                Text.Position = new Vector2f(
                    Position.X,
                    Position.Y - Text.GetGlobalBounds().Height / 2
                );
                break;
        }
        BaseRect.Position = Text.Position;
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
        if (TickVar != null)
        {
            Text.DisplayedString = DisplayString.Replace("%v", TickVar().ToString());
        }
        RenderQueue.QueueGUI(Text);
        SetAnchorPosition();
        if (TickVar != null)
        {
            FloatRect globalBounds = Text.GetGlobalBounds();
            BaseRect.Size = new Vector2f(
                globalBounds.Width,
                globalBounds.Height*1.25f
            );
        }
    }
}
