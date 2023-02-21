using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class BuildMenu : Menu
{

    Dictionary<Structure, Func<Structure>> structures = new Dictionary<Structure, Func<Structure>>
    {
        {new Miner(), () => new Miner()},
        {new Chest(), () => new Chest()}
    };

    public BuildMenu(Vector2f position, Player player, Action closeAction)
    : base("Build", position)
    {
        foreach (KeyValuePair<Structure, Func<Structure>> entry in structures)
        {
            IconButton button = new IconButton(entry.Key.texture, () => {
                player.enterNewState(PlayerState.BuildState.BuildInstance);
                PlayerState.BuildState.BuildInstance.enterBuild(entry.Value());
                }, title: entry.Key.name);
            addRow(new List<GUIActor>{button});
            button.buttonClicked += closeAction;
        }
        closeButton.buttonClicked += closeAction;
        Margin = 110;
        edgeMargin = 75;
        marginOffsetY = 0;
    }
}