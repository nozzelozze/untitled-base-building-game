using System;
using SFML.Graphics;
using SFML.System;

public class Tile
{

    public enum Type
    {
        Dirt,
        Ore
    }

    private Texture Texture;
    public Sprite Sprite;
    public bool Occupied = false;
    public Resource? Resource = null;

    public Tile(Type tileType)
    {
        if (tileType == Type.Dirt)
        {
            Texture = new Random().Next(5) == 1 ? ResourceLoader.FetchTexture(ResourceLoader.TextureType.DirtTwo) : ResourceLoader.FetchTexture(ResourceLoader.TextureType.DirtOne);
        }
        this.Sprite = new Sprite(Texture);
    }

    public bool IsOccupied()
    {
        return Occupied;
    }

    public bool IsWalkable()
    {
        if (Resource == null && Occupied == false)
        {
            return true;
        }
        return false;
    }

    public void GiveResource(Resource newResource)
    {
        Resource = newResource;
        //Occupied = true;
    }

    public bool HasResource()
    {
        if (Resource != null) return true;
        return false;
    }

    public void FreeFromResource(bool stillOccupied = false)
    {
        Resource = null;
        //Occupied = stillOccupied ? true : false;
    }

    public void Render(Vector2f position)
    {
        Sprite.Position = position;
        RenderQueue.Queue(Sprite);
    }
}
