using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class PlayerState
{

    public static PlayerState Instance = new PlayerState();

    public virtual void onPlayerClick(Player player) {
        if (PlayerMouse.onUI) return;
    }
    public virtual void enter() {}
    public virtual void leave() {}
    public virtual void update(Player player) {}

    public class IdleState : PlayerState
    {
        public static IdleState IdleInstance = new IdleState();

        public override void onPlayerClick(Player player)
        {
            base.onPlayerClick(player);
            if (PlayerMouse.onUI) return;
            Player.playerHighlight.unhightlight();
            Structure? clickedStructure = Map.Instance.getStructureFromTile(Map.Instance.getTileAt(Camera.winPositionToCam((Vector2f)PlayerMouse.getPosition())));
            if (clickedStructure != null)
            {
                Player.playerHighlight.highlight(
                    clickedStructure.highlight, 
                    () => {}, 
                    clickedStructure.Position, 
                    new Vector2f(clickedStructure.size.X*Map.tileSize, clickedStructure.size.Y*Map.tileSize), 
                    clickedStructure.renderHighlight
                    );
            }
            Tile clickedTile = Map.Instance.getTileAt(Camera.winPositionToCam((Vector2f)PlayerMouse.getPosition()));
            if (clickedTile.isOccupied() == false && clickedTile.hasResource())
            {
                Player.playerHighlight.highlight(
                    clickedTile.resource.highlight,
                    () => {},
                    Map.Instance.getTilePosition(clickedTile),
                    new Vector2f(Map.tileSize, Map.tileSize),
                    () => clickedTile.resource.clickMenu.render()
                );
            }
        }
    }

    public class BuildState : PlayerState
    {
        public static BuildState BuildInstance = new BuildState();

        Structure wantedStructure = new Chest();

        public void enterBuild(Structure wanted)
        {
            base.enter();
            wantedStructure = wanted;
        }

        public override void update(Player player)
        {
            base.update(player);
            if (PlayerMouse.onUI) return;
            Vector2f structurePosition = Map.Instance.getTilePosition(player.mouse.getTileFromMouse());
            wantedStructure.sprite.Position = structurePosition;
            wantedStructure.Position = structurePosition;
            if (wantedStructure.isCurrentlyValid())
            {
                wantedStructure.sprite.Color = GUIColor.validGreenColor;
            } else
            {
                wantedStructure.sprite.Color = GUIColor.invalidRedColor;
            }
            RenderQueue.queue(wantedStructure.sprite);
        }

        public override void onPlayerClick(Player player)
        {
            base.onPlayerClick(player);
            if (wantedStructure.isCurrentlyValid())
            {
                wantedStructure.placeStructure(player.mouse.getTileFromMouse());
                player.enterNewState(IdleState.IdleInstance);
            }
        }
    }
}