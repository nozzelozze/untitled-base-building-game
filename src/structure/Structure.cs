using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Structure : Transformable
{
    public Texture texture;
    public Sprite sprite;
    public Vector2i size;

    public List<Tile> occupiedTiles = new List<Tile>();

    public Menu ? infoMenu;

    public Vector2f structureInfoMenuPosition = new Vector2f(1500, 550);

    public Structure(Texture texture, Vector2i size)
    {
        this.size = size;
        this.texture = texture;

        sprite = new Sprite(texture);

    }

    public virtual void placeStructure(Tile tile)
    {
        Position = Map.Instance.getTilePosition(tile);
        sprite.Position = (Vector2f)Position;
        sprite.Color = Color.White;
        Map.Instance.structures.Add(this);
        Map.Instance.occupyTilesFromStructure(tile, this);
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

    public virtual void highlight()
    {
        infoMenu = new Menu("Mk miner 2", structureInfoMenuPosition);
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
}