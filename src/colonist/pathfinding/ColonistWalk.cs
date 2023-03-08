using System;
using System.Numerics;
using SFML.System;

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
    
    public Vector2f getColonistCenter()
    {
        Vector2f position = new Vector2f
        (
            colonist.Position.X+colonist.sprite.Texture.Size.X/2,
            colonist.Position.Y+colonist.sprite.Texture.Size.Y/2
        );
        return position;
    }

    public void beginWalk(Tile endTile)
    {
        currentPathIndex = 0;
        currentPath = pathfinding.findPath(Map.Instance.getTileAt(colonist.Position), endTile);
        if (!endTile.isWalkable())
        {
            currentPath.Remove(endTile);
        }
        colonist.Position = Map.Instance.getTilePosition(currentPath[0]);
        direction = Map.Instance.getTileCenter(currentPath[currentPathIndex]) - getColonistCenter();
        direction = Normalize(direction);
    }  

    public void update()
    {
        if (currentPathIndex > currentPath.Count-1)
        {
            return;
        }
        if (currentPath.Count != 0)
        {
            Tile beforeTile = Map.Instance.getTileAt(getColonistCenter());
            colonist.Position += direction * 2.5f;
            Tile afterTile = Map.Instance.getTileAt(getColonistCenter());
            Vector2f tilePosition = Map.Instance.getTilePosition(currentPath[currentPathIndex]);
            Vector2f distance = new Vector2f(tilePosition.X+Map.tileSize/2, tilePosition.Y+Map.tileSize/2) - getColonistCenter();
            if (Math.Abs(new Vector2(distance.X, distance.Y).Length()) < 2.5f)
            {
                currentPathIndex ++;
                if (currentPathIndex > currentPath.Count-1)
                {
                    colonist.walkDone();
                    return;
                }
                direction = Map.Instance.getTileCenter(currentPath[currentPathIndex]) - getColonistCenter();
                direction = Normalize(direction);
            }
        }
    }
}