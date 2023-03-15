using System;
using SFML.System;

public class BuildMenu : Menu
{

    Dictionary<Structure, Func<Structure>> Structures = new Dictionary<Structure, Func<Structure>>
    {
        {new Miner(), () => new Miner()},
        {new Chest(), () => new Chest()},
        {new ConveyorBelt(), () => new ConveyorBelt()}
    };

    public BuildMenu(Vector2f position, Player player, Action closeAction)
    : base("Build", position)
    {
        foreach (KeyValuePair<Structure, Func<Structure>> entry in Structures)
        {
            IconButton button = new IconButton(entry.Key.Texture, () => {
                player.EnterNewState(PlayerState.BuildState.BuildInstance);
                PlayerState.BuildState.BuildInstance.EnterBuild(entry.Value());
                }, title: entry.Key.Name);
            AddRow(new List<GUIActor>{button});
            button.ButtonClicked += closeAction;
        }
        CloseButton.ButtonClicked += closeAction;
        Margin = 110;
        EdgeMargin = 75;
        MarginOffsetY = 0;
    }
}
