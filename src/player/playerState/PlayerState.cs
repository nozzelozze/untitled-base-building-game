using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class PlayerState
{

    public static PlayerState Instance = new PlayerState();

    public virtual void onPlayerClick(Player player) {}
    public virtual void enter() {}
    public virtual void leave() {}
    public virtual void update(Player player) {}

    public class IdleState : PlayerState
    {
        public static IdleState IdleInstance = new IdleState();

        public override void onPlayerClick(Player player)
        {
            base.onPlayerClick(player);
            Structure? clickedStructure = Map.Instance.getStructureFromTile(Map.Instance.getTileAt(Camera.winPositionToCam((Vector2f)PlayerMouse.getPosition())));
            if (clickedStructure != null)
            {
                clickedStructure.highlight();
            }
        }
    }

    public class BuildState : PlayerState
    {
        public static BuildState BuildInstance = new BuildState();

        Structure wantedStructure;

        public void enterBuild(Structure wanted)
        {
            base.enter();
            wantedStructure = wanted;
        }

        public override void update(Player player)
        {
            base.update(player);
            Vector2f structurePosition = Map.Instance.getTilePosition(player.mouse.getTileFromMouse());
            wantedStructure.sprite.Position = structurePosition;
            wantedStructure.Position = structurePosition;
            if (wantedStructure.isCurrentlyValid())
            {
                wantedStructure.sprite.Color = Color.Green;
            } else
            {
                wantedStructure.sprite.Color = Color.Red;
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