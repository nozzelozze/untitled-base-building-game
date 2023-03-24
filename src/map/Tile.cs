using System;
using SFML.Graphics;
using SFML.System;

public class Tile
{

    public enum Type
    {
        Dirt1,
        Dirt2,
        Grass,
        Ore
    }

    private Texture Texture;
    public Sprite Sprite;
    public bool Occupied = false;
    public Resource? Resource = null;

    public Tile(Type tileType)
    {
        Texture = GetTexture(tileType);
        this.Sprite = new Sprite(Texture);
    }

    private Texture GetTexture(Type tileType)
    {
        switch (tileType)
        {
            case Type.Dirt1:
                return ResourceLoader.FetchTexture(ResourceLoader.TextureType.DirtOne);
            case Type.Dirt2:
                return ResourceLoader.FetchTexture(ResourceLoader.TextureType.DirtTwo);
            case Type.Grass:
                return ResourceLoader.FetchTexture(ResourceLoader.TextureType.Grass);
        }
        return ResourceLoader.FetchTexture(ResourceLoader.TextureType.DefaultTexture);
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
