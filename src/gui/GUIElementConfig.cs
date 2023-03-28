using System;
using SFML.Graphics;
using SFML.System;

public class GUIElementConfig
{
    public Vector2f StartPosition { get; set; }
    public StyleManager ? Style  { get; set; }
    public Vector2f ? Size { get; set; }
    public Vector2f ? RelativeTo { get; set; }
    public Func<Vector2f> ? GetRelativeTo { get; set; }
}