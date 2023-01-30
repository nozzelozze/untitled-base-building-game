using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class PlayerState
{
    public virtual void onPlayerClick() {}

    public static class IdleState : PlayerState
    {

    }

    public static class BuildState : PlayerState
    {
        public static override void onPlayerClick()
        {

        }
    }
}