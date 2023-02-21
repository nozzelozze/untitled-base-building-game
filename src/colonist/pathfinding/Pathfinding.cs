using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

public class Pathfinding
{
    
    public List<Tile> getPathTiles(Tile startTile, Tile endTile)
    {
        PriorityQueue<Tile, float> discoveredTiles = new PriorityQueue<Tile, float>();
        discoveredTiles.Enqueue(startTile, 0f); 
        List<Tile> closedTiles = new List<Tile>();

        Dictionary<Tile, float> costs = new Dictionary<Tile, float>{
            {startTile, 0f}
        };

        while (discoveredTiles.Count > 0)
        {
            Tile currentTile = discoveredTiles.Dequeue();
            Console.WriteLine($"Checking current tile {currentTile}");
            foreach(var i in closedTiles) Console.WriteLine(i);
            if (currentTile == endTile)
            {
                constructPath();
                Log.Message("hejsan");
                return new List<Tile>();
            }
            List<Tile> neighborTiles = getNeighborTiles(currentTile);
            foreach (Tile neighborTile in neighborTiles)
            {
                if (!closedTiles.Contains(neighborTile))
                {
                    float distance = distanceCost(neighborTile, endTile) + distanceCost(currentTile, neighborTile);
                    discoveredTiles.Enqueue(neighborTile, distance);
                }
            }
            closedTiles.Add(currentTile);
        }
        return new List<Tile>();
    }

    public float distanceCost(Tile firstTile, Tile secondTile)
    {
        Vector2 distance;
        Tuple<int, int> firstTileIndex = Map.Instance.getTileIndex(firstTile);
        Tuple<int, int> secondTileIndex = Map.Instance.getTileIndex(secondTile);
        distance = 
            new Vector2(
            firstTileIndex.Item1, 
            firstTileIndex.Item2)
            - 
            new Vector2(
                secondTileIndex.Item1, 
                secondTileIndex.Item2);
        return Math.Abs(distance.Length());
    }

    public void constructPath()
    {

    }

    public List<Tile> getNeighborTiles(Tile startTile)
    {
        List<Tile> neighborTiles = new List<Tile>();
        Tuple<int, int> tileIndex = Map.Instance.getTileIndex(startTile);
        for (int x = tileIndex.Item1-1; x < tileIndex.Item1+1; x++)
        {
            for (int y = tileIndex.Item2-1; y < tileIndex.Item2+1; y++)
            {
                if (x < 0 || x > Map.Instance.tiles.GetLength(1) || y < 0 || y > Map.Instance.tiles.GetLength(1))
                {
                    continue;
                }
                if (Map.Instance.tiles[x, y] != startTile) neighborTiles.Add(Map.Instance.tiles[x, y]);
            }   
        }
        return neighborTiles;
    }
}