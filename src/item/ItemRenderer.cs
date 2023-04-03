using SFML.Graphics;
using SFML.System;

public static class ItemRenderer
{

    public static void Render(Item item, Vector2f position, float progress, ConveyorBelt conveyorBelt)
    {
        Sprite sprite = new Sprite(item.GetTexture());

        position += progress * Map.TileSize * DirectionToVector2f(conveyorBelt.ConveyorDirection);
        sprite.Position = position;

        RenderQueue.Queue(sprite);
    }

    private static Vector2f DirectionToVector2f(ConveyorBelt.Direction direction)
    {
        switch (direction)
        {
            case ConveyorBelt.Direction.Up:
                return new Vector2f(0, -1);
            case ConveyorBelt.Direction.Down:
                return new Vector2f(0, 1);
            case ConveyorBelt.Direction.Left:
                return new Vector2f(-1, 0);
            case ConveyorBelt.Direction.Right:
                return new Vector2f(1, 0);
            default:
                return new Vector2f(0, 0);
        }
    }

}