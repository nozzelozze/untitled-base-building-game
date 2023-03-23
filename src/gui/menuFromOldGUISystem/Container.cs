using System;
using SFML.System;

/* public class Container : GUIActor
{

    List<List<GUIActor>> Items;
    public int Margin;
    public int EdgeMargin
    {
        get
        {
            return this.Margin;
        }
        set
        {
            this.Margin = value;
        }
    }
    public float MarginOffsetX;
    public float MarginOffsetY;

    public enum AlignType
    {
        Center,
        Left,
        Right
    }

    AlignType alignType;

    public const int DefaultSizeX = 350;
    public const int DefaultSizeY = 450;

    public bool HasStaticSize = false;

    public Container(Vector2f position, AlignType alignType, bool isTransparent = false,
    List<List<GUIActor>>? containerItems = null, int containerMargin = 65, Vector2f? staticSize = null,
    float marginOffsetX = 0, float marginOffsetY = 0
    ) :
    base(new Vector2f(DefaultSizeX, DefaultSizeY), position, isTransparent)
    {
        if (containerItems == null)
            Items = new List<List<GUIActor>>();
        else
            Items = containerItems;

        Margin = containerMargin;
        EdgeMargin = containerMargin;

        if (staticSize != null)
        {
            BaseRect.Size = staticSize.Value;
            HasStaticSize = true;
        }

        MarginOffsetX = marginOffsetX;
        MarginOffsetY = marginOffsetY;

        this.alignType = alignType;
    }

    public void AddRow(List<GUIActor> newRow)
    {
        Items.Add(newRow);
    }

    public void AddToRow(int rowIndex, GUIActor newItem)
    {
        Items[rowIndex].Add(newItem);
    }

    public void SetStaticSize(Vector2f newSize)
    {
        HasStaticSize = true;
        BaseRect.Size = newSize;
    }

    private Vector2f AlignTypeOffset(AlignType alignType, Vector2f position)
    {
        Vector2f newPosition =
        alignType == AlignType.Center ? Position + new Vector2f(BaseRect.Size.X / 2, EdgeMargin) :
        alignType == AlignType.Left ? Position + new Vector2f(BaseRect.Size.X / 4, EdgeMargin) :
        alignType == AlignType.Right ? Position + new Vector2f(BaseRect.Size.X / (4 / 3), EdgeMargin) : Position;
        return newPosition;
    }

    public override void Render()
    {
        Vector2f currentItemPosition = AlignTypeOffset(alignType, Position);
        currentItemPosition += new Vector2f(MarginOffsetX, MarginOffsetY);
        base.Render();
        int rowCount = 0;
        int itemCount = 0;
        foreach (List<GUIActor> row in Items)
        {
            rowCount++;
            foreach (GUIActor item in row)
            {
                item.Position = new Vector2f(currentItemPosition.X - item.GetSize().X / 2, currentItemPosition.Y);
                item.Render();
                currentItemPosition.X += Margin + item.GetSize().X;
                itemCount++;
            }
            currentItemPosition.Y += Margin;
            currentItemPosition.X = AlignTypeOffset(alignType, Position).X;
        }
        if (!HasStaticSize)
        {
            BaseRect.Size = new Vector2f(
                itemCount * EdgeMargin + EdgeMargin * 2,
                rowCount * EdgeMargin + EdgeMargin * 2
            );
        }
    }
}
 */