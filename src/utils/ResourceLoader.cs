using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class ResourceLoader
{

    private static string texturesPath = "assets/imgs/";
    private static string fontsPath = "assets/fonts/";

    public enum TextureType
    {
        Stone,
        Grass,
        Bed,
        Crosshair,
        Icon,
        CloseIcon,
        Copper,
        Iron,
        Colonist
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
        loadedTextures.Add(TextureType.Icon, new Texture(getTexturePath("icon")));
        loadedTextures.Add(TextureType.CloseIcon, new Texture(getTexturePath("closeWindowIcon")));
        loadedTextures.Add(TextureType.Copper, new Texture(getTexturePath("copper")));
        loadedTextures.Add(TextureType.Iron, new Texture(getTexturePath("iron")));
        loadedTextures.Add(TextureType.Colonist, new Texture(getTexturePath("colonist")));

        // fonts
        fonts.Add("default", new Font(fontsPath + "default.ttf"));
    }

    public Func<string, string> getTexturePath = (name) => texturesPath + name + ".png";

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