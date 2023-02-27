using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class Colonist : Transformable
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

    public StorageComponent storageComponent = new StorageComponent(50);
    public ColonistWalk walk;
    Job currentJob;

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

    public void mineResource(Resource resource)
    {
        Tile tile = Map.Instance.getTileAt(resource.position);
        currentJob = new StorageJob(tile, this);
        currentJob.beginJob();
    }

    public void walkDone()
    {
        currentJob.doJob();
    }

    public void update()
    {
        render();
        walk.update();
        if (Input.events.Contains(Keyboard.Key.Space))
        {
            currentJob = new Job(Map.Instance.tiles[0, 0], this);
            currentJob.doJob();
        }
    }

}