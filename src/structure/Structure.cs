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
        foreach (Structure structure in Map.Instance.Structures)
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

    public virtual void PlaceStructure(Tile tile, bool instaBuild = false)
    {
        Position = Map.Instance.GetTilePosition(tile);
        Sprite.Position = Position;
        MouseCollideRect.Top = Position.Y;
        MouseCollideRect.Left = Position.X;
        Map.Instance.OccupyTilesFromStructure(tile, this);
        Sprite.Color = new Color(200, 200, 200, 205);
        Map.Instance.Structures.Add(this);
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
        Map.Instance.Structures.Remove(this);
    }

    public virtual void Highlight()
    {
        GUIText StructureName = new GUIText(
            Name,
            new GUIElementConfig{ Style = StyleManager.WhiteBackgroundBlackText },
            hasBackgroundColor: true
            );
        if (!Built)
        {
            //InfoContainer.AddElement(new GUIText("Needs resources:"));

            foreach (KeyValuePair<Item.Type, int> costPair in Deposit)
            {
                //InfoMenu.AddItem(new GUIText($"{Item.ItemNames[costPair.Key]}: {costPair.Value} / {Cost[costPair.Key]}"));
            }

            //InfoMenu.AddItem(new TextButton("Cancel Build", () => CancelBuild()));
        }
        //InfoMenu.CloseButton.ButtonClicked += Player.PlayerHighlight.Unhighlight;
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