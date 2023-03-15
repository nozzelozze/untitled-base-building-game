using System;
using SFML.Window;
using SFML.System;

public class PlayerState
{

    public static PlayerState Instance = new PlayerState();

    public virtual void OnPlayerClick(Player player) {
        if (PlayerMouse.OnUI) return;
    }
    public virtual void Enter() {}
    public virtual void Leave() {}
    public virtual void Update(Player player) {}

    public class IdleState : PlayerState
    {
        public static IdleState IdleInstance = new IdleState();

        public override void OnPlayerClick(Player player)
        {
            base.OnPlayerClick(player);
            if (PlayerMouse.OnUI) return;
            Player.PlayerHighlight.Unhighlight();
            Structure? clickedStructure = Map.Instance.GetStructureFromTile(Map.Instance.GetTileAt(Camera.WinPositionToCam((Vector2f)PlayerMouse.GetPosition())));
            if (clickedStructure != null)
            {
                Player.PlayerHighlight.Highlight(
                    clickedStructure.Highlight, 
                    () => {}, 
                    () => clickedStructure.Position, 
                    new Vector2f(clickedStructure.Size.X*Map.TileSize, clickedStructure.Size.Y*Map.TileSize), 
                    clickedStructure.RenderHighlight
                );
            }
            Tile clickedTile = Map.Instance.GetTileAt(Camera.WinPositionToCam((Vector2f)PlayerMouse.GetPosition()));
            if (clickedTile.IsOccupied() == false && clickedTile.HasResource())
            {
                Player.PlayerHighlight.Highlight(
                    clickedTile.Resource.Highlight,
                    () => {},
                    () => Map.Instance.GetTilePosition(clickedTile),
                    new Vector2f(Map.TileSize, Map.TileSize),
                    () => clickedTile.Resource.ClickMenu.Render()
                );
            }
        }
    }

    public class BuildState : PlayerState
    {
        public static BuildState BuildInstance = new BuildState();

        Structure WantedStructure = new Chest();

        public void EnterBuild(Structure wanted)
        {
            base.Enter();
            WantedStructure = wanted;
        }

        public override void Update(Player player)
        {
            base.Update(player);
            if (PlayerMouse.OnUI) return;
            Vector2f structurePosition = Map.Instance.GetTilePosition(player.PlayerMouse.GetTileFromMouse());
            WantedStructure.Sprite.Position = structurePosition;
            WantedStructure.Position = structurePosition;
            if (WantedStructure.IsCurrentlyValid())
            {
                WantedStructure.Sprite.Color = GUIColor.ValidGreenColor;
            } else
            {
                WantedStructure.Sprite.Color = GUIColor.InvalidRedColor;
            }
            RenderQueue.Queue(WantedStructure.Sprite);
            if (Input.Events.Contains(Mouse.Button.Right))
            {
                player.EnterNewState(IdleState.IdleInstance);
            }
        }

        public override void OnPlayerClick(Player player)
        {
            base.OnPlayerClick(player);
            if (WantedStructure.IsCurrentlyValid())
            {
                WantedStructure.PlaceStructure(player.PlayerMouse.GetTileFromMouse());
                player.EnterNewState(IdleState.IdleInstance);
            }
        }
    }
}
