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
                //Vector2f a = nextTilePos - afterTilePos;
                //float distanceToNextTile = new Vector2(a.X, a.Y).Length();
public void update()
{
    if (currentPath.Count != 0)
    {
        Vector2f currentPosition = colonist.Position;
        Vector2f nextPosition = currentPosition + direction;
        Tile currentTile = Map.Instance.getTileAt(currentPosition);
        Tile nextTile = Map.Instance.getTileAt(nextPosition);
        
        if (currentTile != nextTile)
        {
            colonist.Position = Map.Instance.getTilePosition(nextTile);
            currentPathIndex++;

            if (currentPathIndex < currentPath.Count - 1)
            {
                direction = Map.Instance.getTilePosition(currentPath[currentPathIndex + 1]) - colonist.Position;
                direction = Normalize(direction);
            }
            else
            {
                currentPath.Clear();
                return;
            }
        }

        colonist.Position += direction;
    }
}
}