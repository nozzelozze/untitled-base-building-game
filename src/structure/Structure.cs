using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Structure : Transformable
{
    public Texture texture;
    public Sprite sprite;
    public Vector2i size;

    public List<Tile> occupiedTiles = new List<Tile>();

    bool highlighted = false;
    public Menu infoMenu;
    RectangleShape highlightRect;

    public Vector2f structureInfoMenuPosition = new Vector2f(1500, 550);

    public Structure(Texture texture, Vector2i size)
    {
        this.size = size;
        this.texture = texture;

        sprite = new Sprite(texture);
        highlightRect = new RectangleShape((Vector2f)texture.Size);
        highlightRect.OutlineColor = GUIColor.textColor;
        highlightRect.OutlineThickness = GUIActor.outlineThickness;
        highlightRect.FillColor = Color.Transparent;
    }

    public void placeStructure(Tile tile)
    {
        Position = Map.Instance.getTilePosition(tile);
        sprite.Position = (Vector2f)Position;
        sprite.Color = Color.White;
        Map.Instance.structures.Add(this);
        Map.Instance.occupyTilesFromStructure(tile, this);
    }

    public virtual void highlight()
    {
        infoMenu = new Menu("Mk miner 2", structureInfoMenuPosition);
        infoMenu.closeButton.buttonClicked += minimize;
        highlightRect.Position = Position;
        highlighted = true;
    }

    public void minimize()
    {
        highlighted = false;
    }

    private void render()
    {
        RenderQueue.queue(sprite);
        if (highlighted)
        {
            infoMenu.render();
            RenderQueue.queue(highlightRect);
        }
    }

    public virtual void update()
    {
        render();
    }
}