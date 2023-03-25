using System;
using SFML.System;
using SFML.Window;

public class DefaultInterface
{
    
    //IconButton BuildButton;
    //BuildMenu? BuildMenu;

    IconButton MineButton;
    IconButton BuildButton;

    MouseBox MouseBox;

    Player Player;

    public DefaultInterface(Player player)
    {
        this.Player = player;
        //BuildButton = new IconButton(
        //    ResourceLoader.FetchTexture(ResourceLoader.TextureType.BuildButton), 
        //    OnBuildButtonClick,
        //    new Vector2f(50, 50),
        //    "Build"
        //);

    MineButton = new IconButton(new Vector2f(100, 1080-100), () => 
    {
        Player.EnterNewState(PlayerState.CreateState("Build")); PlayerState.BuildState.GetWantedStructure = () => new Chest();
    }, ResourceLoader.FetchTexture(ResourceLoader.TextureType.MineButton), "Mine");
        MouseBox = new MouseBox();
    }

    public void OnBuildButtonClick()
    {
        //BuildMenu = new BuildMenu(
        //    BuildButton.Position + new Vector2f(75, 0),
        //    Player,
        //    () => BuildMenu = null
        //);
    }

    public void Update()
    {
        //BuildButton.Render();
        //if (BuildMenu != null) BuildMenu.Render();
        MouseBox.Position = (Vector2f)PlayerMouse.GetPosition() + new Vector2f(25, 25);
    }
}
