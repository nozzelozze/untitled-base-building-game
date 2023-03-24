using System;
using SFML.System;

class Map
{
    public static Map Instance = new Map();

    public static int TileSize = 64;
    const int MapWidth = 64;
    const int MapHeight = 64;
    public Tile[,] Tiles = new Tile[MapWidth,MapHeight];
    public List<Structure> Structures = new List<Structure>();

    private Map()
    {
        MapGenerator mapGenerator = new MapGenerator(MapWidth, MapHeight, seed: 123313);
        Tiles = mapGenerator.GenerateMap();
    }

    public void OccupyTile(Tile tile)
    {
        tile.Occupied = true;
    }

    public void OccupyTilesFromStructure(Tile tile, Structure structure)
    {
        Tuple<int, int> tileIndex = GetTileIndex(tile);
        for (int i = tileIndex.Item1; i < structure.Size.X + tileIndex.Item1; i++)
        {
            for (int j = tileIndex.Item2; j < structure.Size.Y + tileIndex.Item2; j++)
            {
                OccupyTile(Tiles[i, j]);
                structure.OccupiedTiles.Add(Tiles[i, j]);
            }
        }
    }

    public Structure? GetStructureFromTile(Tile tile)
    {
        foreach (Structure structure in Structures)
        {
            foreach (Tile structureTile in structure.OccupiedTiles)
            {
               if (structureTile == tile) return structure;
            }
        }
        //Log.Warning("Structure is not found from tile. (GetStructureFromTile)");
        return null;
    }

    public Tuple<int, int> GetTileIndex(Tile tile)
    {
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                if (Tiles[i, j] == tile)
                {
                    return new Tuple<int, int>(i, j);
                }
            }
        }
        Log.Error($"Could not find tile index. {tile}");
        return new Tuple<int, int>(0, 0);
    }

    public void Render()
    {
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                Vector2f tilePosition = new Vector2f((float)x*TileSize, (float)y*TileSize);
                if (!Tiles[x, y].IsOccupied() && !Tiles[x, y].HasResource())
                {
                    Tiles[x,y].Render(tilePosition);
                }
                if (Tiles[x, y].HasResource())
                {
                    Tiles[x, y].Resource?.Render();
                }
            }
        }
        foreach (Structure structure in Structures)
        {
            structure.Update();
        }
    }
    
    public Vector2f GetTilePosition(Tile tile)
    {
        Vector2f position = new Vector2f();
        Tuple<int, int> tileIndex = GetTileIndex(tile);
        position.X = tileIndex.Item1 * TileSize;
        position.Y = tileIndex.Item2 * TileSize;
        return position;
    }

    public Vector2f GetTileCenter(Tile tile)
    {
        Vector2f tilePosition = GetTilePosition(tile);
        Vector2f center = new Vector2f
        (
            tilePosition.X + TileSize / 2,
            tilePosition.Y + TileSize / 2
        );
        return center;
    }

    public Tile GetTileAt(Vector2f getPosition)
    {
        Func<int, int, int> nearestMultiple = (numToRound, multiple) => numToRound > 0 ? numToRound - (numToRound % multiple) : 0;
        int nearestRowTile = nearestMultiple((int)getPosition.X, TileSize);
        int nearestColumnTile = nearestMultiple((int)getPosition.Y, TileSize);
        return Tiles[(int)nearestRowTile/MapWidth, (int)nearestColumnTile/MapHeight];
    }
}