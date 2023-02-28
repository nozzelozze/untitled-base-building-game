using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

/* THIS IS EXTREMELY BAD AND NOT GOOD CODE */

public class ColonistWalk
{
    private List<Tile> currentPath = new List<Tile>();
    private int currentPathIndex = 0;
    private Pathfinding pathfinding = new Pathfinding();
    private Vector2f direction = new Vector2f();
    private Colonist colonist;
    private bool isWalking = false;

    public ColonistWalk(Colonist colonist)
    {
        this.colonist = colonist;
    }

    private static Vector2f Normalize(Vector2f vector)
    {
        float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

        if (length != 0)
        {
            vector.X /= length;
            vector.Y /= length;
        }

        return vector;
    }

    public void beginWalk(Tile endTile)
    {
        currentPathIndex = 0;
        currentPath = pathfinding.findPath(Map.Instance.getTileAt(colonist.Position), endTile);
        currentPath.RemoveAt(currentPath.Count-1);
        foreach (Tile s in currentPath)
        {
            s.sprite.Color = GUIColor.validGreenColor;
        }

        if (currentPath.Count > 1)
        {
            colonist.Position = Map.Instance.getTilePosition(currentPath[0]);
            direction = Map.Instance.getTilePosition(currentPath[1]) - Map.Instance.getTilePosition(currentPath[0]);
            direction = Normalize(direction);
            isWalking = true;
        }
    }

    public void update()
    {
        if (!isWalking)
        {
            return;
        }

        Tile currentTile = Map.Instance.getTileAt(colonist.Position);
        colonist.Position += direction;
        Map.Instance.getTileAt(colonist.Position).sprite.Color = GUIColor.invalidRedColor;
        if (currentTile != Map.Instance.getTileAt(colonist.Position))
        {
            currentPathIndex++;

            if (currentPathIndex >= currentPath.Count-1)
            {
                currentPath.Clear();
                isWalking = false;
                colonist.walkDone();
                return;
            }
            direction = Map.Instance.getTilePosition(currentPath[currentPathIndex+1 < currentPath.Count ? currentPathIndex+1 : currentPath.Count-1]) - Map.Instance.getTilePosition(currentPath[currentPathIndex-1]);
            direction = Normalize(direction);
        }
    }
}