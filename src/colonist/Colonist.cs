using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Colonist : Transformable
{

    private static Dictionary<int, Colonist> Colonists = new Dictionary<int, Colonist>();
    public static Colonist PullColonist(int colonistId)
    {
        if (!Colonists.ContainsKey(colonistId))
        {
            Log.Error($"Colonist with the id {colonistId} doesn't exist.");
            return null;
        }
        return Colonists[colonistId];
    }

    FloatRect CollisionRect;
    Texture Texture;
    public Sprite Sprite;

    public StorageComponent StorageComponent = new StorageComponent(50);
    public ColonistJobManager PersonalJobManager;
    public ColonistWalk Walk;

    Menu? InfoMenu;

    public Colonist(int id)
    {
        Colonists.Add(id, this);
        Walk = new ColonistWalk(this);
        PersonalJobManager = new ColonistJobManager(this);

        Texture = ResourceLoader.FetchTexture(ResourceLoader.TextureType.Colonist);
        Sprite = new Sprite(Texture);
        CollisionRect.Width = Texture.Size.X;
        CollisionRect.Height = Texture.Size.Y;
        Random random = new Random();
        int x, y;
        do
        {
            x = random.Next(0, 750);
            y = random.Next(0, 750);
            Position = new Vector2f(x, y);
        } while (!Map.Instance.GetTileAt(Position).IsWalkable());
    }

    public void Render()
    {
        Sprite.Position = Position;
        RenderQueue.Queue(Sprite);
    }

    public void WalkDone()
    {
        PersonalJobManager.WorkCurrentJob();
    }

    public void Highlight()
    {
        InfoMenu = new Menu("Peter xDDD", Menu.InfoMenuPosition);
        InfoMenu.AddItem(new GUIText("Inventory: %v", tickVar: () => StorageComponent.GetItems().Count((item) => item.ItemType == Item.Type.Iron)));
        InfoMenu.CloseButton.ButtonClicked += Player.PlayerHighlight.Unhighlight;
    }

    public void Update()
    {
        Render();
        Walk.Update();
        Vector2i mousePosition = PlayerMouse.GetPosition();
        Vector2f collisionPosition = Camera.CamPositionToWin(Position);
        CollisionRect.Top = collisionPosition.Y;
        CollisionRect.Left = collisionPosition.X;
        if (CollisionRect.Contains(mousePosition.X, mousePosition.Y))
        {
            if (Input.Events.Contains(Mouse.Button.Left) && Player.CurrentState == PlayerState.IdleState.IdleInstance)
            {
                Input.Events.Remove(Mouse.Button.Left);
                Player.PlayerHighlight.Highlight(
                    Highlight,
                    () => { },
                    () => Position,
                    (Vector2f)Sprite.Texture.Size,
                    () => InfoMenu.Render()
                );
            }
        }

        if (StorageComponent.IsFull())
        {
            PersonalJobManager.EmptyStorage();
        }

        PersonalJobManager.Update();
    }
}
