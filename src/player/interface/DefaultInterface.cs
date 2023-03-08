using System;
using SFML.System;

public class DefaultInterface
{
    
    IconButton buildButton;
    BuildMenu ? buildMenu;


    Player player;

    public DefaultInterface(Player player)
    {
        this.player = player;
        buildButton = new IconButton(
            ResourceLoader.fetchTexture(ResourceLoader.TextureType.BuildButton), 
            onBuildButtonClick,
            new Vector2f(50, 50),
            "Build"
        );
    }

    public void onBuildButtonClick()
    {
        buildMenu = new BuildMenu(
            buildButton.Position + new Vector2f(75, 0),
            player,
            () => buildMenu = null
        );
    }

    public void update()
    {
        buildButton.render();
        if (buildMenu != null) buildMenu.render();
    }

}