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
        loadedTextures.Add(TextureType.Stone, new Texture(texturesPath + "stoneTile.png"));
        loadedTextures.Add(TextureType.Grass, new Texture(texturesPath + "grassTile.png"));
        loadedTextures.Add(TextureType.Bed, new Texture(texturesPath + "bed.png"));
        loadedTextures.Add(TextureType.Crosshair, new Texture(texturesPath + "crosshair.png"));

        // fonts
        fonts.Add("default", new Font(fontsPath + "default.ttf"));
    }

    public static Font fetchFont(string fontName)
    {
        if (!fonts.ContainsKey(fontName))
        {
            Log.Error($"{fontName} does not exist.");
            return null;
        }
        return fonts[fontName];
    }

    public static Texture fetchTexture(TextureType type)
    {
        if (!loadedTextures.ContainsKey(type))
        {
            Log.Error($"{type} does not exist.");
            return null;
        }
        return loadedTextures[type];
    }
}