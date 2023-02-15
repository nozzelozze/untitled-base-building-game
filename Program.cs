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

        Container myContainer = new Container(
            new Vector2f(500f, 500f),
            true,
            new List<List<GUIActor>>{
                new List<GUIActor>
                {
                    new IconButton(
                        ResourceLoader.fetchTexture(ResourceLoader.TextureType.Icon),
                        new Vector2f(500, 500),
                        () => Log.Message("Clicked a button!")
                        ),
                    new IconButton(
                        ResourceLoader.fetchTexture(ResourceLoader.TextureType.Icon),
                        new Vector2f(500, 500),
                        () => Log.Message("Clicked a button!")
                        ),
                    new IconButton(
                        ResourceLoader.fetchTexture(ResourceLoader.TextureType.Icon),
                        new Vector2f(500, 500),
                        () => Log.Message("Clicked a button!")
                        )
                },
                new List<GUIActor>{
                                        new IconButton(
                        ResourceLoader.fetchTexture(ResourceLoader.TextureType.Icon),
                        new Vector2f(500, 500),
                        () => Log.Message("Clicked a button!")
                        ),
                                            new IconButton(
                        ResourceLoader.fetchTexture(ResourceLoader.TextureType.Icon),
                        new Vector2f(500, 500),
                        () => Log.Message("Clicked a button!")
                        )
                }
            }
        );

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

            myContainer.render();

            RenderQueue.render(window, uiView);
            window.Display();
        }
    }
}
