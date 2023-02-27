using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Tile
{

    public enum Type
    {
        Dirt,
        Ore
    }

    private Texture texture;
    public Sprite sprite;
    public bool occupied = false;
    public Resource ? resource = null;

    public Tile(Type tileType)
    {
        if (tileType == Type.Dirt)
        {
            texture = new Random().Next(5) == 1 ? ResourceLoader.fetchTexture(ResourceLoader.TextureType.DirtTwo) : ResourceLoader.fetchTexture(ResourceLoader.TextureType.DirtOne);
        }
        this.sprite = new Sprite(texture);
    }

    public bool isOccupied()
    {
        return occupied;
    }

    public override string ToString()
    {
        return $"{Map.Instance.getTilePosition(this)}";
    }

    public bool isWalkable()
    {
        if (resource == null && occupied == false)
        {
            return true;
        }
        return false;
    }

    public void giveResource(Resource newResource)
    {
        resource = newResource;
        //occupied = true;
    }

    public bool hasResource()
    {
        if (resource != null) return true;
        return false;
    }

    public void freeFromResource(bool stillOccupied = false)
    {
        resource = null;
        //occupied = stillOccupied ? true : false;
    }

    public void render(Vector2f position)
    {
        sprite.Position = position;
        RenderQueue.queue(sprite);
    }

}