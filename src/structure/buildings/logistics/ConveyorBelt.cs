using System;
using SFML.System;
using System.Collections.Generic;

public class ConveyorBelt : Structure
{
    
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Undefined
    }
    
    public Direction ConveyorDirection;
    public Queue<Tuple<Item, float>> ItemsOnBelt;
    private const float ItemMoveTime = 1f;
    private float timeElapsed;

    private Clock UpdateClock;

    public ConveyorBelt()
    : base ("Conveyor Belt", ResourceLoader.FetchTexture(ResourceLoader.TextureType.Copper), new Vector2i(1, 1), new Dictionary<Item.Type, int>())
    {
        ConveyorDirection = Direction.Undefined;
        ItemsOnBelt = new Queue<Tuple<Item, float>>();
        UpdateClock = new Clock();
    }

    public void SetDirectionBasedOnNeighbours()
    {
        // Add this later, rotation will work for now
        /*         Tile upTile = Map.Instance.GetAdjacentTile(OccupiedTiles[0], Direction.Up);
        Tile downTile = Map.Instance.GetAdjacentTile(OccupiedTiles[0], Direction.Down);
        Tile leftTile = Map.Instance.GetAdjacentTile(OccupiedTiles[0], Direction.Left);
        Tile rightTile = Map.Instance.GetAdjacentTile(OccupiedTiles[0], Direction.Right);

        if (upTile != null && Map.Instance.GetStructureFromTile(upTile) is ConveyorBelt)
        {
            ConveyorDirection = Direction.Up;
        }
        else if (downTile != null && Map.Instance.GetStructureFromTile(downTile) is ConveyorBelt)
        {
            ConveyorDirection = Direction.Down;
        }
        else if (leftTile != null && Map.Instance.GetStructureFromTile(leftTile) is ConveyorBelt)
        {
            ConveyorDirection = Direction.Left;
        }
        else if (rightTile != null && Map.Instance.GetStructureFromTile(rightTile) is ConveyorBelt)
        {
            ConveyorDirection = Direction.Right;
        }
        //ConveyorDirection = Direction.Right;
        Console.WriteLine(ConveyorDirection); */
    }

    public override void PlaceStructure(Tile tile, bool instaBuild = true)
    {
        base.PlaceStructure(tile, instaBuild);
        SetDirectionBasedOnNeighbours();
    }

    public void AddItem(Item item)
    {
        ItemsOnBelt.Enqueue(new Tuple<Item, float>(item, 0f));
    }

    public Item RemoveItem()
    {
        if (ItemsOnBelt.Count > 0)
        {
            return ItemsOnBelt.Dequeue().Item1;
        }
        return null;
    }

    public bool IsEmpty()
    {
        return ItemsOnBelt.Count == 0;
    }

    public override void Update()
    {
        base.Update();

        // Move items on the conveyor belt over time
        float deltaTime = UpdateClock.Restart().AsSeconds(); // Calculate deltaTime
        //timeElapsed += deltaTime;
        //if (timeElapsed >= ItemMoveTime)
        //{
        //    timeElapsed = 0;

            // Implement logic to move items between conveyor belts
        //}

        List<Tuple<Item, float>> newItemsOnBelt = new List<Tuple<Item, float>>();
        foreach (Tuple<Item, float> itemTuple in ItemsOnBelt)
        {
            Item item = itemTuple.Item1;

            float progress = itemTuple.Item2 + deltaTime / ItemMoveTime;
            if (progress >= 1f)
            {
                
                progress = 1f;

                ConveyorBelt? nextConveyor = Map.Instance.StructureManager.GetAdjacentStructure<ConveyorBelt>(StartTile, ConveyorDirection);

                if (nextConveyor != null)
                {
                    nextConveyor.AddItem(item);
                } else
                {
                    newItemsOnBelt.Add(Tuple.Create(item, progress));
                }

            } else
            {
                newItemsOnBelt.Add(Tuple.Create(item, progress));
            }
            ItemRenderer.Render(item, Position, progress, this);
        }
        ItemsOnBelt = new Queue<Tuple<Item, float>>(newItemsOnBelt);
    }
    
}
