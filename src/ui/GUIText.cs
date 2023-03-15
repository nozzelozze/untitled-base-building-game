using System;
using SFML.Graphics;
using SFML.System;

public class GUIText : GUIActor
{

    Text Text;
    Func<object>? TickVar;
    string DisplayString;

    public GUIText(string text, CharacterSize charSize = CharacterSize.HeadingSmall, Func<object>? tickVar = null)
    : base(new Vector2f(), hasOutline: false, isTransparent: true)
    {
        DisplayString = text;
        Text = new Text(text, ResourceLoader.FetchFont("default"));
        Text.CharacterSize = GetCharacterSize(charSize);
        TickVar = tickVar;
        if (!text.Contains("%v") && tickVar != null)
        {
            Log.Error($"'{text}' does not include '%v' but a tickVariable is given.");
        }

        FloatRect globalBounds = Text.GetGlobalBounds();
        BaseRect.Size = new Vector2f(
            globalBounds.Width,
            globalBounds.Height
        );
    }

    public override void Render()
    {
        base.Render();
        Text.Position = Position;
        if (TickVar != null)
        {
            Text.DisplayedString = DisplayString.Replace("%v", TickVar().ToString());
        }
        RenderQueue.QueueGUI(Text);
    }
}
