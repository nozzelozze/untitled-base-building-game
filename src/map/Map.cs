using System;
using System.Linq;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Map
{
    public static Map Instance = new Map();

    public static int tileSize = 64;
    const int mapWidth = 64;
    const int mapHeight = 64;
    public Tile[ , ] tiles = new Tile[mapWidth,mapHeight];
    public List<Structure> structures = new List<Structure>();

    private Map()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tiles[x,y] = new Tile(new Texture(new Random().Next(2) == 0 ? "assets/imgs/stoneTile.png" : "assets/imgs/grassTile.png"));
            }
        }
    }

    public void occupyTile(Tile tile)
    {
        tile.occupied = true;
    }

    public void occupyTilesFromStructure(Tile tile, Structure structure)
    {
        Tuple<int, int> tileIndex = getTileIndex(tile);
        for (int i = tileIndex.Item1; i < structure.size.X+tileIndex.Item1; i++)
        {
            for (int j = tileIndex.Item2; j < structure.size.Y+tileIndex.Item2; j++)
            {
                occupyTile(tiles[i, j]);
                structure.occupiedTiles.Add(tiles[i, j]);
            }
        }
    }

    public Structure ? getStructureFromTile(Tile tile)
    {
        foreach (Structure structure in structures)
        {
            foreach (Tile structureTile in structure.occupiedTiles)
            {
               if (structureTile == tile) return structure;
            }
        }
        //Log.Warning("Structure is not found from tile. (getStructureFromTile)");
        return null;
    }

    public Tuple<int, int> getTileIndex(Tile tile)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j] == tile)
                {
                    return new Tuple<int, int>(i, j);
                }
            }
        }
        Log.Error($"Could not find tile index. {tile}");
        return new Tuple<int, int>(0, 0);
    }

    public void render()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector2f tilePosition = new Vector2f((float)x*tileSize, (float)y*tileSize);
                if (!tiles[x, y].isOccupied() && !tiles[x, y].hasResource())
                {
                    tiles[x,y].render(tilePosition);
                }
                if (tiles[x, y].hasResource())
                {
                    tiles[x, y].resource?.render();
                }
            }
        }
        foreach (Structure structure in structures)
        {
            structure.update();
        }
    }
    
    public Vector2f getTilePosition(Tile tile)
    {
        Vector2f position = new Vector2f();
        Tuple<int, int> tileIndex = getTileIndex(tile);
        position.X = tileIndex.Item1*tileSize;
        position.Y = tileIndex.Item2*tileSize;
        return position;
    }

    public Tile getTileAt(Vector2f getPosition)
    {
        Func<int, int, int> nearestMultiple = (numToRound, multiple) => numToRound > 0 ? numToRound - (numToRound % multiple) : 0;
        int nearestRowTile = nearestMultiple((int)getPosition.X, tileSize);
        int nearestColumnTile = nearestMultiple((int)getPosition.Y, tileSize);
        return tiles[(int)nearestRowTile/mapWidth, (int)nearestColumnTile/mapHeight];
    }
}