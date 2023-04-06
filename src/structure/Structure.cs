using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Linq;

public class Structure : Transformable
{
    public Sprite Sprite;
    public Vector2i Size;

    public Tile StartTile;

    public Texture Texture = ResourceLoader.FetchTexture(ResourceLoader.TextureType.DefaultTexture);
    public string Name = "Structure";

    public FloatRect MouseCollideRect;

    public List<Tile> OccupiedTiles = new List<Tile>();

    private bool HasRotated = false;

    public Container InfoContainer;

    public bool Built = false;

    public Dictionary<Item.Type, int> Cost;
    public Dictionary<Item.Type, int> Deposit;

    public Structure(string structName, Texture structTexture, Vector2i size, Dictionary<Item.Type, int> cost)
    {
        Size = size;
        Texture = structTexture;
        Name = structName;

        Sprite = new Sprite(Texture);

        Cost = cost;

        Deposit = Cost.Keys.ToDictionary(key => key, key => 0);
        
        InfoContainer = new Container(new GUIElementConfig());
        Player.Instance.DefaultInterface.TopRightContainer.AddElement(InfoContainer);

        MouseCollideRect = new FloatRect(0, 0, size.X*Map.TileSize, size.Y*Map.TileSize);

    }

    public static T? GetNearestStructure<T>() where T : Structure
    {
        foreach (Structure structure in Map.Instance.StructureManager.Structures)
        {
            if (structure.Built)
            {
                if (structure is T)
                {
                    return (T)structure;
                }
            }
        }
        return null;
    }

    public virtual void Rotate()
    {
        if (!Built)
        {
            int oldX = Size.X;
            int oldY = Size.Y;
            Size = new Vector2i(oldY, oldX); // swap x & y
            if (!HasRotated)
            {
                Sprite.Origin = new Vector2f(0, Sprite.Texture.Size.Y);
                Sprite.Rotation += 90;
                HasRotated = true;
            } else
            {
                Sprite.Origin = new Vector2f(0, 0);
                Sprite.Rotation -= 90;
                HasRotated = false;
            }
        }
    }

    public virtual void PlaceStructure(Tile tile, bool instaBuild = true)
    {
        Position = Map.Instance.GetTilePosition(tile);
        Sprite.Position = Position;
        MouseCollideRect.Top = Position.Y;
        MouseCollideRect.Left = Position.X;
        Map.Instance.OccupyTilesFromStructure(tile, this);
        Sprite.Color = new Color(200, 200, 200, 205);
        Map.Instance.StructureManager.Structures.Add(this);
        Player.PlayerHighlight.Unhighlight();
        StartTile = tile;
        if (instaBuild)
        {
            Build();
        }
        else
        {
            JobManager.AddToQueue(new BuildJob(this));
        }
    }

    public void Build()
    {
        Built = true;
        Sprite.Color = Color.White;
        SoundManager.PlaySFX(SoundManager.SoundType.Build);
    }

    public virtual bool IsTileValid(Tile tile)
    {
        return !tile.IsOccupied();
    }

    public bool IsCurrentlyValid()
    {
        Tuple<int, int> firstTileIndex = Map.Instance.GetTileIndex(Map.Instance.GetTileAt(Position));

        for (int x = firstTileIndex.Item1; x < firstTileIndex.Item1 + Size.X; x++)
        {
            for (int y = firstTileIndex.Item2; y < firstTileIndex.Item2 + Size.Y; y++)
            {
                if (IsTileValid(Map.Instance.Tiles[x, y]) == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool WantMenu()
    {
        //return InfoMenu != null && Built;
        return false;
    }

    public bool IsPaidFor()
    {
        return Cost.OrderBy(x => x.Key).SequenceEqual(Deposit.OrderBy(x => x.Key)) || Built;
    }

    public void CancelBuild()
    {
        Player.PlayerHighlight.Unhighlight();
        foreach (Tile tile in OccupiedTiles)
        {
            tile.Occupied = false;
        }
        Map.Instance.StructureManager.Structures.Remove(this);
    }

    public virtual void Highlight()
    {
        GUIText StructureName = new GUIText(
            Name,
            new GUIElementConfig{ Style = StyleManager.WhiteBackgroundBlackText },
            hasBackgroundColor: true
            );
        InfoContainer.AddElement(StructureName);
        if (!Built)
        {
            InfoContainer.AddElement(new GUIText("Needs resources:", new GUIElementConfig()));

            foreach (KeyValuePair<Item.Type, int> costPair in Deposit)
            {
                InfoContainer.AddElement(new GUIText($"{Item.ItemNames[costPair.Key]}: {costPair.Value} / {Cost[costPair.Key]}", new GUIElementConfig()));
            }

            InfoContainer.AddElement(new LabelButton(new GUIElementConfig(), () => CancelBuild(), "Cancel Build"));
        }
    }

    public void RenderHighlight()
    {
        //InfoMenu?.Render();
    }

    private void Render()
    {
        RenderQueue.Queue(Sprite);
    }

    public virtual void Update()
    {
        Render();
        if (IsPaidFor() && !Built)
        {
            Build();
        }
        InfoContainer.ClearElements();
        Vector2i mousePosition = PlayerMouse.GetPosition();
        Vector2f mouseCollPos = Camera.CamPositionToWin(Position);
        MouseCollideRect.Left = mouseCollPos.X;
        MouseCollideRect.Top = mouseCollPos.Y; 
        if (MouseCollideRect.Contains(mousePosition.X, mousePosition.Y))
        {
            Highlight();
        }
    }

    public void Tick()
    {
        if (Built)
        {
            Update();
        }
        else
        {
            Render();
        }
    }
}
