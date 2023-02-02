using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Game
{
    const int WIDTH = 1920;
    const int HEIGHT = 1080;
    const string TITLE = "cazza";

    static void Main(string[] args)
    {
        VideoMode mode = new VideoMode(WIDTH, HEIGHT);
        RenderWindow window = new RenderWindow(mode, TITLE);

        Camera camera = new Camera(new View(window.GetView()));
        View uiView = new View(window.GetView());
        Input input = new Input();
        Player player = new Player(camera);
        ResourceLoader resourceLoader = new ResourceLoader();

        GUIActor gui = new GUIActor(new Vector2f(100f, 100f), new Vector2f(500f, 500f));

        window.SetVerticalSyncEnabled(true);
        window.Closed += (sender, args) => window.Close();
        window.MouseWheelScrolled += camera.scroll;

        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear(Color.Black);
            input.getEvents();

            camera.updateCamera(window);
            Map.Instance.render();
            player.updatePlayer(window);

            gui.render();

            RenderQueue.render(window, uiView);
            window.Display();
        }
    }
}
