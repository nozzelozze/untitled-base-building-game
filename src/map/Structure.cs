using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Structure
{
    public Texture texture = new Texture("imgs/bed.png");
    public Sprite sprite;
    public Vector2i position;
    public Vector2i size = new Vector2i(2, 1);

    public Structure()
    {
        sprite = new Sprite(texture);
    }

    public void placeStructure(Tile tile)
    {
        position = (Vector2i)Map.Instance.getTilePosition(tile);
        sprite.Position = (Vector2f)position;
        sprite.Color = Color.White;
        Map.Instance.structures.Add(this);
        Map.Instance.occupyTilesFromStructure(tile, this);
    }
}