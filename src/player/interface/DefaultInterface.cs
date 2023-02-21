using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class DefaultInterface
{
    
    IconButton buildButton;
    Player player;

    public DefaultInterface(Player player)
    {
        this.player = player;
        buildButton = new IconButton(
            ResourceLoader.fetchTexture(ResourceLoader.TextureType.BuildButton), 
            onBuildButtonClick,
            new Vector2f(100, 100),
            "Build"
        );
    }

    public void onBuildButtonClick()
    {
        player.enterNewState(PlayerState.BuildState.BuildInstance);
        PlayerState.BuildState.BuildInstance.enterBuild(new Miner());
    }

    public void update()
    {
        buildButton.render();
    }

}