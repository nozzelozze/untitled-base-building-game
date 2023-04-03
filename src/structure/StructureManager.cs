using System;
using SFML.Graphics;
using SFML.System;
using System.Numerics;

public class StructureManager
{

    public List<Structure> Structures { get; private set; } = new List<Structure>();
    private Map map;

    public StructureManager(Map map)
    {
        this.map = map;
    }

    public void UpdateStructures()
    {
        foreach (Structure structure in Structures)
        {
            structure.Update();
        }
    }

    public T ?  GetAdjacentStructure<T>(Tile tile, ConveyorBelt.Direction direction) where T : Structure
    {
        Vector2i adjacentTileIndex = GetAdjacentTileCoords(tile, direction);
        Tile adjacentTile = map.Tiles[adjacentTileIndex.X, adjacentTileIndex.Y];

        if (adjacentTile != null)
        {
            foreach (Structure structure in adjacentTile.OccupyingStructures)
            {
                if (structure is T)
                {
                    return (T)structure;
                }
            }
        }

        return null;
    }

    public Vector2i GetAdjacentTileCoords(Tile tile, ConveyorBelt.Direction direction)
    {
        Tuple<int, int> tileIndex = map.GetTileIndex(tile);
        Vector2i newTileIndex = new Vector2i(tileIndex.Item1, tileIndex.Item2);

        switch (direction)
        {
            case ConveyorBelt.Direction.Up:
                newTileIndex.Y -= 1;
                break;
            case ConveyorBelt.Direction.Down:
                newTileIndex.Y += 1;
                break;
            case ConveyorBelt.Direction.Left:
                newTileIndex.X -= 1;
                break;
            case ConveyorBelt.Direction.Right:
                newTileIndex.X += 1;
                break;
        }

        return newTileIndex;
    }

}