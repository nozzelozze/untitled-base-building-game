using System;
using SFML.Window;
using SFML.System;

/* public abstract class PlayerState
{

    public virtual void OnPlayerClick(Player player) {
        if (PlayerMouse.OnUI) return;
    }
    public virtual void Enter() {}
    public virtual void Leave() {}
    public virtual void Update(Player player) {}
    public abstract string GetName(); 

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
                    new Vector2f(Map.TileSize, Map.TileSize), () => {}
                    //() => clickedTile.Resource.ClickMenu.Render()
                );
            }
        }
    }

    public class SelectState : PlayerState
    {
        public static SelectState SelectInstance = new SelectState();

        public override void Update(Player player)
        {
            base.Update(player);
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
                WantedStructure.Sprite.Color = StyleManager.DarkBlueColor;
            } else
            {
                WantedStructure.Sprite.Color = StyleManager.DarkRedColor;
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
 */

public abstract class PlayerState
{
    public virtual void OnPlayerClick(Player player, Mouse.Button button) {
        if (PlayerMouse.OnUI) return;
    }
    public virtual void OnPlayerRelease(Player player, Mouse.Button button) {}
    public virtual void Enter() {}
    public virtual void Leave() {}
    public virtual void Update(Player player) {}
    public abstract string GetName();

    // Add a factory method to create PlayerStates based on their names
    public static PlayerState CreateState(string stateName)
    {
        switch (stateName)
        {
            case "Idle":
                return new IdleState();
            case "Build":
                return new BuildState();
            // Add more cases here as you expand your game
            default:
                throw new ArgumentException($"Invalid state name: {stateName}");
        }
    }

    public class IdleState : PlayerState
    {
        public override string GetName() => "Idle";

        // (...)
    }

    public class BuildState : PlayerState
    {
        public Structure ? WantedStructure { private get; set; }
        public static Func<Structure> ? GetWantedStructure;

        public BuildState()
        {
            
        }

        public override string GetName() => "Build";

        private void UpdateStructurePosition(Vector2f position, Player player)
        {
            Vector2f structurePosition = Map.Instance.GetTilePosition(player.PlayerMouse.GetTileFromMouse());
            WantedStructure.Sprite.Position = structurePosition;
            WantedStructure.Position = structurePosition;
        }

        private void UpdateStructureColor(bool isValid)
        {
            WantedStructure.Sprite.Color = isValid ? SFML.Graphics.Color.Green : StyleManager.DarkRedColor;
        }

        public override void Update(Player player)
        {
            if (PlayerMouse.OnUI) return;
            if (WantedStructure == null) WantedStructure = GetWantedStructure();
            Vector2f structurePosition = Map.Instance.GetTilePosition(player.PlayerMouse.GetTileFromMouse());
            UpdateStructurePosition(structurePosition, player);
            bool isValid = WantedStructure.IsCurrentlyValid();
            UpdateStructureColor(isValid);
            RenderQueue.Queue(WantedStructure.Sprite);
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (WantedStructure.IsCurrentlyValid())
                {
                    WantedStructure.PlaceStructure(player.PlayerMouse.GetTileFromMouse());
                    if (GetWantedStructure != null) WantedStructure = GetWantedStructure();
                    //player.EnterNewState(PlayerState.CreateState("Idle"));
                }
            }
        }

        public override void OnPlayerRelease(Player player, Mouse.Button button)
        {
            base.OnPlayerRelease(player, button);
            if (button == Mouse.Button.Right && !player.IsPanning)
            {
                player.EnterNewState(PlayerState.CreateState("Idle"));
            }
        }

    }
}