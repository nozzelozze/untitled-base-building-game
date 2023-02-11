using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Structure
{
    public Texture texture = ResourceLoader.fetchTexture(ResourceLoader.TextureType.Bed);
    public Sprite sprite;
    public Vector2i position;
    public Vector2i size = new Vector2i(2, 1);

    public List<Tile> occupiedTiles = new List<Tile>();

    public Container ? infoMenu; /* Här borde det vara en separat klass InfoMenu som ärver från bas Container. */

    public Vector2f structureInfoPosition = new Vector2f(1500, 550);

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

    public void showInfo()
    {
        infoMenu = new Container("Bed ._. :D:D:D", structureInfoPosition);
        infoMenu.closeButton.buttonClicked += hideInfo;
    }

    public void hideInfo()
    {
        infoMenu = null;
    }

    public void render()
    {
        RenderQueue.queue(sprite);
        if (infoMenu != null) infoMenu.render();
    }
}