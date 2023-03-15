using System;
using System.Numerics;
using SFML.System;

public class ColonistWalk
{
    private List<Tile> CurrentPath = new List<Tile>();
    private int CurrentPathIndex = 0;
    private Pathfinding Pathfinding = new Pathfinding();
    private Vector2f Direction = new Vector2f();
    private Colonist Colonist;
    public ColonistWalk(Colonist colonist)
    {
        this.Colonist = colonist;
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
    
    public Vector2f GetColonistCenter()
    {
        Vector2f position = new Vector2f
        (
            Colonist.Position.X+Colonist.Sprite.Texture.Size.X/2,
            Colonist.Position.Y+Colonist.Sprite.Texture.Size.Y/2
        );
        return position;
    }

    public void BeginWalk(Tile endTile)
    {
        CurrentPathIndex = 0;
        CurrentPath = Pathfinding.FindPath(Map.Instance.GetTileAt(Colonist.Position), endTile);
        if (!endTile.IsWalkable())
        {
            CurrentPath.Remove(endTile);
        }
        Colonist.Position = Map.Instance.GetTilePosition(CurrentPath[0]);
        Direction = Map.Instance.GetTileCenter(CurrentPath[CurrentPathIndex]) - GetColonistCenter();
        Direction = Normalize(Direction);
    }  

    public void Update()
    {
        if (CurrentPathIndex > CurrentPath.Count-1)
        {
            return;
        }
        if (CurrentPath.Count != 0)
        {
            Tile beforeTile = Map.Instance.GetTileAt(GetColonistCenter());
            Colonist.Position += Direction * 2.5f;
            Tile afterTile = Map.Instance.GetTileAt(GetColonistCenter());
            Vector2f tilePosition = Map.Instance.GetTilePosition(CurrentPath[CurrentPathIndex]);
            Vector2f distance = new Vector2f(tilePosition.X+Map.TileSize/2, tilePosition.Y+Map.TileSize/2) - GetColonistCenter();
            if (Math.Abs(new Vector2(distance.X, distance.Y).Length()) < 2.5f)
            {
                CurrentPathIndex ++;
                if (CurrentPathIndex > CurrentPath.Count-1)
                {
                    Colonist.WalkDone();
                    return;
                }
                Direction = Map.Instance.GetTileCenter(CurrentPath[CurrentPathIndex]) - GetColonistCenter();
                Direction = Normalize(Direction);
            }
        }
    }
}
