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

        Camera camera = new Camera(window.GetView());
        Input input = new Input();
        Player player = new Player(camera);

        Window testWindow = new Window(new Vector2f(200, 200));

        window.SetVerticalSyncEnabled(true);
        window.Closed += (sender, args) => window.Close();
        window.MouseWheelScrolled += camera.scroll;

        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear(Color.Black);
            List<object> events = input.getEvents();
            camera.updateCamera(window, events);
            Map.Instance.render();
            player.updatePlayer(events);
            testWindow.render();
            RenderQueue.render(window);
            window.Display();
        }
    }
}
