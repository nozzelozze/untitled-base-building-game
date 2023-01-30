using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Tile
{
    private Texture texture;
    private Sprite sprite;
    public bool occupied = false;

    public Tile(Texture texture)
    {
        this.texture = texture;
        this.sprite = new Sprite(texture);
    }

    public void render(Vector2f position)
    {
        sprite.Position = position;
        RenderQueue.queue(sprite);
    }

}