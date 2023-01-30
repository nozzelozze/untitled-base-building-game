using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Structure
{
    public Texture texture = new Texture("imgs/bed.png");
    public Sprite sprite;
    public Vector2i position;
    public Vector2i size = new Vector2i(2, 1);

    public Structure(Tile tile)
    {
        sprite = new Sprite(texture);
        position = (Vector2i)Map.Instance.getTilePosition(tile);
        sprite.Position = (Vector2f)position;
        Tuple<int, int> tileIndex = Map.Instance.getTileIndex(tile);
        Map.Instance.occupyTilesFromStructure(tileIndex.Item1, tileIndex.Item2, this);
    }
}