using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class Pathfinding
{
    public List<Tile> findPath(Tile startTile, Tile endTile)
    {
        var openSet = new List<Tile>() { startTile };
        var cameFrom = new Dictionary<Tile, Tile>();
        var gScore = new Dictionary<Tile, float>() { { startTile, 0 } };
        var fScore = new Dictionary<Tile, float>() { { startTile, Heuristic(startTile, endTile) } };

        while (openSet.Count > 0)
        {
            var currentTile = openSet.OrderBy(t => fScore.ContainsKey(t) ? fScore[t] : float.MaxValue).First();

            if (currentTile == endTile)
            {
                return ReconstructPath(cameFrom, currentTile);
            }

            openSet.Remove(currentTile);

            foreach (var neighborTile in GetNeighborTiles(currentTile))
            {
                if (currentTile.isWalkable())
                {
                    var tentativeGScore = gScore[currentTile] + DistanceCost(currentTile, neighborTile);

                    if (!gScore.ContainsKey(neighborTile) || tentativeGScore < gScore[neighborTile])
                    {
                        cameFrom[neighborTile] = currentTile;
                        gScore[neighborTile] = tentativeGScore;
                        fScore[neighborTile] = tentativeGScore + Heuristic(neighborTile, endTile);

                        if (!openSet.Contains(neighborTile))
                        {
                            openSet.Add(neighborTile);
                        }
                    }
                }
            }
        }
        return new List<Tile>();
    }

    private List<Tile> GetNeighborTiles(Tile startTile) // partly chatgpt
    {
        List<Tile> neighborTiles = new List<Tile>();
        Tuple<int, int> tileIndex = Map.Instance.getTileIndex(startTile);
        
        // Check for neighboring tiles in the four cardinal directions only (not diagonals)
        int[,] directions = {
            {0, -1}, // North
            {0, 1},  // South
            {-1, 0}, // West
            {1, 0}   // East
        };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int x = tileIndex.Item1 + directions[i, 0];
            int y = tileIndex.Item2 + directions[i, 1];

            if (x < 0 || x >= Map.Instance.tiles.GetLength(0) || y < 0 || y >= Map.Instance.tiles.GetLength(1))
            {
                continue;
            }

            if (Map.Instance.tiles[x, y] != startTile) neighborTiles.Add(Map.Instance.tiles[x, y]);
        }

        return neighborTiles;
    }

    private float DistanceCost(Tile firstTile, Tile secondTile)
    {
        Vector2 distance;
        Tuple<int, int> firstTileIndex = Map.Instance.getTileIndex(firstTile);
        Tuple<int, int> secondTileIndex = Map.Instance.getTileIndex(secondTile);
        distance = new Vector2(
            firstTileIndex.Item1,
            firstTileIndex.Item2)
            -
            new Vector2(
                secondTileIndex.Item1,
                secondTileIndex.Item2);
        return Math.Abs(distance.Length());
    }

    private float Heuristic(Tile startTile, Tile endTile)
    {
        return DistanceCost(startTile, endTile);
    }

    private List<Tile> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile currentTile)
    {
        var totalPath = new List<Tile>() { currentTile };
        while (cameFrom.ContainsKey(currentTile))
        {
            currentTile = cameFrom[currentTile];
            totalPath.Insert(0, currentTile);
        }
        return totalPath;
    }
}