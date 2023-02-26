using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

class Colonist : Transformable
{

    private static Dictionary<int, Colonist> colonists = new Dictionary<int, Colonist>();

    public static Colonist pullColonist(int colonistId)
    {
        if (!colonists.ContainsKey(colonistId))
        {
            Log.Error($"Colonist with the id {colonistId} doesn't exist.");
            return null;
        }
        return colonists[colonistId];
    }

    Texture texture;
    Sprite sprite;

    StorageComponent storageComponent = new StorageComponent(50);
    ColonistWalk walk;

    public Colonist(int id)
    {
        colonists.Add(id, this);
        walk = new ColonistWalk(this);

        texture = ResourceLoader.fetchTexture(ResourceLoader.TextureType.Colonist);
        sprite = new Sprite(texture);
        Random random = new Random();
        int x, y;
        do
        {
            x = random.Next(0, 750);
            y = random.Next(0, 750);
            Position = new Vector2f(x, y);
        } while (!Map.Instance.getTileAt(Position).isWalkable());
    }

    public void render()
    {
        sprite.Position = Position;
        RenderQueue.queue(sprite);
    }

    public void beginWalk(Tile endTile)
    {
        walk.beginWalk(endTile);
    }

    public void update()
    {
        render();
        walk.update();
    }

}