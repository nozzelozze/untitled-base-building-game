using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class PlayerState
{

    public static PlayerState Instance = new PlayerState();

    public virtual void onPlayerClick() {}
    public virtual void enter() {}
    public virtual void leave() {}
    public virtual void update(Player player) {}

    public class IdleState : PlayerState
    {
        public static IdleState IdleInstance = new IdleState();
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
            wantedStructure.sprite.Position = Map.Instance.getTilePosition(player.mouse.getTileFromMouse());
            RenderQueue.queue(wantedStructure.sprite);
        }

        public override void onPlayerClick()
        {
            base.onPlayerClick();
            Console.WriteLine("Clicked while in build state.");
        }
    }
}