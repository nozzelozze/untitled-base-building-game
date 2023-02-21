using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Container : GUIActor
{

    List<List<GUIActor>> items;
    private int margin;
    public int Margin
    {
        get
        {
            return this.margin;
        }
        set
        {
            this.margin = value;
        }
    }
    public float marginOffsetX;
    public float marginOffsetY;
    public int edgeMargin;

    public enum AlignType
    {
        Center,
        Left,
        Right
    }

    AlignType alignType;

    public const int defaultSizeX = 350;
    public const int defaultSizeY = 450;

    public bool hasStaticSize = false;
    
    public Container(Vector2f position, AlignType alignType, bool isTransparent = false, 
    List<List<GUIActor>> ? containerItems = null, int containerMargin = 65, Vector2f ? staticSize = null, 
    float marginOffsetX = 0, float marginOffsetY = 0
    ) : 
    base(new Vector2f(defaultSizeX, defaultSizeY), position, isTransparent)
    {
        if (containerItems == null)
            items = new List<List<GUIActor>>();
        else
            items = containerItems;
        
        margin = containerMargin;
        edgeMargin = containerMargin;

        if (staticSize != null)
        {
            baseRect.Size = staticSize.Value;
            hasStaticSize = true;
        }

        this.marginOffsetX = marginOffsetX;
        this.marginOffsetY = marginOffsetY;

        this.alignType = alignType;
    }

    public void addRow(List<GUIActor> newRow)
    {
        items.Add(newRow);
    }

    public void addToRow(int rowIndex, GUIActor newItem)
    {
        items[rowIndex].Add(newItem);
    }

    public void setStaticSize(Vector2f newSize)
    {
        hasStaticSize = true;
        baseRect.Size = newSize;
    }

    public Vector2f alignTypeOffset(AlignType alignType, Vector2f position)
    {
        Vector2f newPosition = 
        alignType == AlignType.Center ? Position + new Vector2f(baseRect.Size.X/2, edgeMargin) : 
        alignType == AlignType.Left ? Position + new Vector2f(baseRect.Size.X/4, edgeMargin) : 
        alignType == AlignType.Right ? Position + new Vector2f(baseRect.Size.X/(4/3), edgeMargin) : Position;
        return newPosition;
    }

    public override void render()
    {
        Vector2f currentItemPosition = alignTypeOffset(alignType, Position);
        currentItemPosition += new Vector2f(marginOffsetX, marginOffsetY);
        base.render();
        int rowCount = 0;
        int itemCount = 0;
        foreach (List<GUIActor> row in items)
        {
            rowCount ++;
            foreach (GUIActor item in row)
            {
                item.Position = new Vector2f(currentItemPosition.X-item.getSize().X/2, currentItemPosition.Y);
                item.render();
                currentItemPosition.X += margin + item.getSize().X;
                itemCount ++;
            }
            currentItemPosition.Y += margin;
            currentItemPosition.X = alignTypeOffset(alignType, Position).X;
        }
        if (!hasStaticSize)
        {
            baseRect.Size = new Vector2f(
                itemCount*edgeMargin + edgeMargin*2,
                rowCount*edgeMargin + edgeMargin*2
            );
        }
    }

}