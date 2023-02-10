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

<<<<<<< HEAD
        Button myButton = new Button("Accept", new Vector2f(1000, 1000), () => Log.Message("hejsan!"));
        Container myContainer = new Container("Mk2 Miner");
=======
        
        //Button myButton = new Button("Accept", new Vector2f(100f, 50f), () => Log.Message("hejsan!"));
        IconButton myButton = new IconButton(
            ResourceLoader.fetchTexture(ResourceLoader.TextureType.Icon),
            new Vector2f(500, 500),
            () => Log.Message("Clicked a button!")
            );
>>>>>>> 026816d328a501c463cbad3fa20c5e9fdf6eab9f

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

<<<<<<< HEAD
            //myButton.render();
=======
            myButton.render();
>>>>>>> 026816d328a501c463cbad3fa20c5e9fdf6eab9f
            //myContainer.render();

            RenderQueue.render(window, uiView);
            window.Display();
        }
    }
}
