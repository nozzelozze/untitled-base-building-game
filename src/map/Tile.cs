using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Tile
{

    public enum Type
    {
        Grass,
        Dirt,
        Ore
    }

    private Texture texture;
    private Sprite sprite;
    public bool occupied = false;
    public Resource ? resource = null;

    public bool isOccupied()
    {
        return occupied;
    }

    public void giveResource(Resource newResource)
    {
        resource = newResource;
        occupied = true;
    }

    public void freeFromResource(bool stillOccupied = false)
    {
        resource = null;
        occupied = stillOccupied ? true : false;
    }

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