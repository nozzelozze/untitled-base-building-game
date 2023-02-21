using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

class Game
{
    const int WIDTH = 1920;
    const int HEIGHT = 1080;
    const string TITLE = "cazza";

    static void Main(string[] args)
    {
        VideoMode mode = new VideoMode(WIDTH, HEIGHT);
        RenderWindow window = new RenderWindow(mode, TITLE);

        ResourceLoader resourceLoader = new ResourceLoader();
        Camera camera = new Camera(new View(window.GetView()));
        View uiView = new View(window.GetView());
        Input input = new Input();
        Player player = new Player(camera);

        Colonist colonist = new Colonist();

        window.SetVerticalSyncEnabled(true);
        window.Closed += (sender, args) => window.Close();
        window.MouseWheelScrolled += camera.scroll;
        for (int x = 0; x < 64; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                if (new Random().Next(7) == 0) new Resource(Resource.ResourceType.Iron, Map.Instance.tiles[x, y]);
            }
        }
        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear(Color.Black);
            input.getEvents();

            camera.updateCamera(window);
            Map.Instance.render();

            colonist.update();

            player.updatePlayer(window);
            RenderQueue.render(window, uiView);
            window.Display();
        }
    }
}
