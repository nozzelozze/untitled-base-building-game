using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class MouseBox : GUIElement
{

    private GUIText InfoText;

    public MouseBox()
    : base((Vector2f)Mouse.GetPosition(), StyleManager.MouseBoxStyle)
    {
        InfoText = new GUIText(
            "%v", 
            (Vector2f)Mouse.GetPosition(), 
            () => Player.Instance.CurrentState.GetName(),
            anchorPoint: GUIText.AnchorPoint.Left,
            hasBackgroundColor: true
        );
    }


    public override void Update()
    {
        base.Update();
        InfoText.Position = Position;
    }

}