using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Structure : Transformable
{
    public Sprite sprite;
    public Vector2i size;

    public Texture texture = ResourceLoader.fetchTexture(ResourceLoader.TextureType.DefaultTexture);
    public string name = "Structure";

    public List<Tile> occupiedTiles = new List<Tile>();

    public Menu ? infoMenu;

    public Vector2f structureInfoMenuPosition = new Vector2f(1500, 550);

    public bool built = false;

    public Dictionary<Item.Type, int> cost;

    public Dictionary<Item.Type, List<Item>> coststs;

    public Structure(string structName, Texture structTexture, Vector2i size, Dictionary<Item.Type, int> cost)
    {
        this.size = size;
        texture = structTexture;
        name = structName;

        sprite = new Sprite(texture);

        this.cost = cost;
    }

    public virtual void placeStructure(Tile tile, bool instaBuild = false)
    {
        Position = Map.Instance.getTilePosition(tile);
        sprite.Position = Position;
        Map.Instance.occupyTilesFromStructure(tile, this);
        sprite.Color = new Color(200, 200, 200, 205);
        Map.Instance.structures.Add(this);
        if (instaBuild) build();
    }

    public void build()
    {
        built = true;
        sprite.Color = Color.White;
        SoundManager.playSFX(SoundManager.SoundType.Build, Position);
    }

    public virtual bool isTileValid(Tile tile)
    {
        return !tile.isOccupied();
    }

    public bool isCurrentlyValid()
    {
        Tuple<int, int> firstTileIndex = Map.Instance.getTileIndex(Map.Instance.getTileAt(Position));

        for (int x = firstTileIndex.Item1; x < firstTileIndex.Item1+size.X; x++)
        {
            for (int y = firstTileIndex.Item2; y < firstTileIndex.Item2+size.Y; y++)
                {
                    if (isTileValid(Map.Instance.tiles[x, y]) == false)
                    {
                        return false;
                    }
                }   
        }
        return true;
    }

    public bool wantMenu()
    {
        return infoMenu != null && built;
    }

    public void cancelBuild()
    {
        Player.playerHighlight.unhightlight();
        foreach(Tile tile in occupiedTiles)
        {
            tile.occupied = false;
        }
        Map.Instance.structures.Remove(this);
    }

    public virtual void highlight()
    {
        infoMenu = new Menu("Structure", structureInfoMenuPosition);
        if (!built)
        {
            infoMenu.addItem(new GUIText("Needs resources:"));

            infoMenu.addItem(new TextButton("Cancel Build", () => cancelBuild()));
        }
        infoMenu.closeButton.buttonClicked += Player.playerHighlight.unhightlight;
    }

    public void renderHighlight()
    {   
        infoMenu?.render();
    }

    private void render()
    {
        RenderQueue.queue(sprite);
    }

    public virtual void update()
    {
        render();
    }

    public void tick()
    {
        if (built)
        {
            update();
        } else
        {
            render();
        }
    }
}