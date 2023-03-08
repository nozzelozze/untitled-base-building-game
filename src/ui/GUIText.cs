using System;
using SFML.Graphics;
using SFML.System;

public class GUIText : GUIActor
{

    Text text;
    Func<object> ? tickVar;
    string displayString;

    public GUIText(string text, characterSize charSize = characterSize.HeadingSmall, Func<object> ? tickVar = null)
    : base(new Vector2f(), hasOutline: false, isTransparent: true)
    {
        displayString = text;
        this.text = new Text(text, ResourceLoader.fetchFont("default"));
        this.text.CharacterSize = getCharacterSize(charSize);
        this.tickVar = tickVar;
        if (!text.Contains("%v") && tickVar != null)
        {
            Log.Error($"'{text}' does not include '%v' but a tickVariable is given.");
        }

        FloatRect globalBounds = this.text.GetGlobalBounds();
        baseRect.Size = new Vector2f(
            globalBounds.Width,
            globalBounds.Height
        );
    }

    public override void render()
    {
        base.render();
        text.Position = Position;
        if (tickVar != null)
        {
            text.DisplayedString = displayString.Replace("%v", tickVar().ToString());
        }
        RenderQueue.queueGUI(text);
    }
}