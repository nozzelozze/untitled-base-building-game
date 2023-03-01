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
        colonist.Position = Map.Instance.getTilePosition(currentPath[0]);
        foreach (Tile s in currentPath)
        {
            s.sprite.Color = GUIColor.validGreenColor;
        }
        direction = Map.Instance.getTilePosition(currentPath[currentPathIndex+1]) - Map.Instance.getTilePosition(currentPath[currentPathIndex]);
        direction = Normalize(direction);
    }

    public void update()
    {

        if (currentPath.Count != 0)
        {
            Tile beforeTile = Map.Instance.getTileAt(colonist.Position);
            colonist.Position += direction;
            Tile afterTile = Map.Instance.getTileAt(colonist.Position);
            if (beforeTile != afterTile)
            {
                Log.Message("entered");
                currentPathIndex ++;
                direction = Map.Instance.getTilePosition(currentPath[currentPathIndex+1]) - Map.Instance.getTilePosition(currentPath[currentPathIndex]);
                direction = Normalize(direction);
            }
        }
    }
}