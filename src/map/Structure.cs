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

    bool highlighted = false;
    Container ? infoMenu; /* Här borde det vara en separat klass InfoMenu som ärver från bas Container. */
    RectangleShape highlightRect;

    public Vector2f structureInfoPosition = new Vector2f(1500, 550);

    int testNumber = 0;

    public Structure()
    {
        sprite = new Sprite(texture);
        highlightRect = new RectangleShape((Vector2f)texture.Size);
        highlightRect.OutlineColor = GUIColor.textColor;
        highlightRect.OutlineThickness = GUIActor.outlineThickness;
        highlightRect.FillColor = Color.Transparent;
    }

    public void placeStructure(Tile tile)
    {
        position = (Vector2i)Map.Instance.getTilePosition(tile);
        sprite.Position = (Vector2f)position;
        sprite.Color = Color.White;
        Map.Instance.structures.Add(this);
        Map.Instance.occupyTilesFromStructure(tile, this);
    }

    public void highlight()
    {
        infoMenu = new Menu("Mk miner 2", structureInfoPosition);
        highlightRect.Position = (Vector2f)position;
        highlighted = true;
    }

    public void minimize()
    {
        infoMenu = null;
        highlighted = false;
    }

    private void render()
    {
        RenderQueue.queue(sprite);
        if (highlighted && infoMenu != null)
        {
            infoMenu.render();
            RenderQueue.queue(highlightRect);
        }
    }

    public void update()
    {
        render();
        testNumber ++;
    }
}