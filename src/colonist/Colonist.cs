using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

class Colonist : Transformable
{

    Texture texture;
    Sprite sprite;

    float dt = 0;

    public Colonist()
    {
        texture = ResourceLoader.fetchTexture(ResourceLoader.TextureType.Colonist);
        sprite = new Sprite(texture);
        Position = new Vector2f(500f, 500f);
    }

    public void render()
    {
        sprite.Position = Position;
        RenderQueue.queue(sprite);
    }

    public void update()
    {
        render();
        //Position = new Vector2f(Position.X+1, Position.Y);
    }

}