using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Container : GUIActor
{

    List<List<GUIActor>> items;
    int margin;

    public enum AlignType
    {
        Center,
        Left,
        Right
    }

    AlignType alignType;

    /* Sizex, sizey should be in a infomenu class?  */

    public const int sizeX = 350;
    public const int sizeY = 450;
    
    public Container(Vector2f position, AlignType alignType, bool isTransparent = false, List<List<GUIActor>> ? containerItems = null, int containerMargin = 65) : base(new Vector2f(sizeX, sizeY), position, isTransparent)
    {
        if (containerItems == null)
            items = new List<List<GUIActor>>();
        else
            items = containerItems;
        
        margin = containerMargin;

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

    public override void render()
    {
        Vector2f currentItemPosition = alignType == AlignType.Center ? Position + new Vector2f(baseRect.Size.X/3, margin) : 
        alignType == AlignType.Left ? Position + new Vector2f(baseRect.Size.X/5, margin) : 
        alignType == AlignType.Right ? Position + new Vector2f(baseRect.Size.X/2, margin) : Position;
        base.render();
        foreach (List<GUIActor> row in items)
        {
            foreach (GUIActor item in row)
            {
                item.Position = currentItemPosition;
                item.render();
                currentItemPosition.X += margin;
            }
            currentItemPosition.Y += margin;
            currentItemPosition.X = Position.X + margin;
        }
    }

}