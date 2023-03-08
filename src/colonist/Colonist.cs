using SFML.Graphics;
using SFML.Window;
using SFML.System;

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

    FloatRect collisionRect;
    Texture texture;
    public Sprite sprite;

    public StorageComponent storageComponent = new StorageComponent(50);

    public ColonistWalk walk;

    Job ? currentJob;
    private List<Job> personalJobQueue = new List<Job>();

    Menu ?  infoMenu;

    public Colonist(int id)
    {
        colonists.Add(id, this);
        walk = new ColonistWalk(this);

        texture = ResourceLoader.fetchTexture(ResourceLoader.TextureType.Colonist);
        sprite = new Sprite(texture);
        collisionRect.Width = texture.Size.X;
        collisionRect.Height = texture.Size.Y;
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

    public void jobDone()
    {
        currentJob = null;
    }

    public void walkDone()
    {
        currentJob?.doJob();
    }

    public void highlight()
    {
        infoMenu = new Menu("Peter xDDD", Menu.infoMenuPosition);
        infoMenu.addItem(new GUIText("Inventory: %v", tickVar: () => storageComponent.getItems().Count((item) => item.type == Item.Type.Iron)));
        infoMenu.closeButton.buttonClicked += Player.playerHighlight.unhighlight;
    }

    public void addToPersonalJobQueue(Job newJob)
    {
        personalJobQueue.Add(newJob);
    }

    public void pushBackCurrentJob()
    {
        personalJobQueue.Insert(0, currentJob);
        currentJob = personalJobQueue[1];
        personalJobQueue.RemoveAt(1);
        currentJob.beginJob(this);
    }

    public void emptyStorage()
    {
        foreach (Job job in personalJobQueue)
        {
            if (job is StorageJob) return;
        }   
        if (currentJob is StorageJob) return;
        //Chest firstChest = Map.Instance.structures.FirstOrDefault(s => s is Chest) as Chest;
        Chest firstChest = Structure.getNearestStructure<Chest>();
        if (firstChest != null) JobManager.addToQueue(new StorageJob(firstChest.storageComponent, firstChest.startTile));
    }

    public void update()
    {
        render();
        walk.update();
        //Console.WriteLine(currentJob);
        Vector2i mousePosition = PlayerMouse.getPosition();
        Vector2f collisionPosition = Camera.camPositionToWin(Position);
        collisionRect.Top = collisionPosition.Y;
        collisionRect.Left = collisionPosition.X;
        if (collisionRect.Contains(mousePosition.X, mousePosition.Y))
        {
            if (Input.events.Contains(Mouse.Button.Left) && Player.currentState == PlayerState.IdleState.IdleInstance)
            {
                Input.events.Remove(Mouse.Button.Left);
                Player.playerHighlight.highlight(
                    highlight,
                    () => {},
                    () => Position,
                    (Vector2f)sprite.Texture.Size,
                    () => infoMenu.render()
                );
            }
        }

        if (storageComponent.isFull())
        {
            emptyStorage();
        }

        if (currentJob == null)
        {
            if (personalJobQueue.Count == 0)
            {
                if (JobManager.jobQueue.Count != 0)
                {
                    personalJobQueue.Add(JobManager.getJob());
                }
            } else
            {
                currentJob = personalJobQueue[0];
                currentJob.beginJob(this);
                personalJobQueue.Remove(currentJob);
            }
        } else
        {
            currentJob.updateJob();
        }

    }
}