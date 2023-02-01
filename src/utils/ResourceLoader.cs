using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class ResourceLoader
{

    private string texturesPath = "assets/imgs/";
    private string fontsPath = "assets/fonts/";

    public enum TextureType
    {
        Stone,
        Grass,
        Bed,
        Crosshair
    }
    private static Dictionary<TextureType, Texture> loadedTextures = new Dictionary<TextureType, Texture>();

    private static Dictionary<string, Font> fonts = new Dictionary<string, Font>();

    public ResourceLoader()
    {
        // textures
        loadedTextures.Add(TextureType.Stone, new Texture(new string(texturesPath + "stoneTile.png")));
        loadedTextures.Add(TextureType.Grass, new Texture(new string(texturesPath + "grassTile.png")));
        loadedTextures.Add(TextureType.Bed, new Texture(new string(texturesPath + "bed.png")));
        loadedTextures.Add(TextureType.Bed, new Texture(new string(texturesPath + "crosshair.png")));

        // fonts
        fonts.Add("default", new Font(fontsPath + "default.ttf"));
    }

    public static Font fetchFont(string fontName)
    {
        if (!fonts.ContainsKey(fontName))
    }

    public static Texture fetchTexture(TextureType type)
    {
        if (!loadedTextures.ContainsKey(type))
        {
            Console.WriteLine($"error {type} does not exist");
            return null;
        }
        return loadedTextures[type];
    }
}