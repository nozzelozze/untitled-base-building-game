using System;
using SFML.System;

public class DefaultInterface
{
    
    IconButton BuildButton;
    BuildMenu? BuildMenu;

    Player Player;

    public DefaultInterface(Player player)
    {
        this.Player = player;
        BuildButton = new IconButton(
            ResourceLoader.FetchTexture(ResourceLoader.TextureType.BuildButton), 
            OnBuildButtonClick,
            new Vector2f(50, 50),
            "Build"
        );
    }

    public void OnBuildButtonClick()
    {
        BuildMenu = new BuildMenu(
            BuildButton.Position + new Vector2f(75, 0),
            Player,
            () => BuildMenu = null
        );
    }

    public void Update()
    {
        BuildButton.Render();
        if (BuildMenu != null) BuildMenu.Render();
    }
}
