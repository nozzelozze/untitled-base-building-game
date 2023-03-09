using System;
using SFML.Graphics;

public class ResourceLoader
{

    private static string texturesPath = "assets/imgs/";
    private static string fontsPath = "assets/fonts/";

    public enum TextureType
    {
        DefaultTexture,
        DirtOne,
        DirtTwo,
        Bed,
        Crosshair,
        CloseIcon,
        Copper,
        Iron,
        Colonist,
        Chest,
        BuildButton
    }
    
    private static Dictionary<TextureType, Texture> loadedTextures = new Dictionary<TextureType, Texture>();

    private static Dictionary<string, Font> fonts = new Dictionary<string, Font>();

    public ResourceLoader()
    {
        // textures
        loadedTextures.Add(TextureType.DirtOne, new Texture(texturesPath + "tiles/dirt1.png"));
        loadedTextures.Add(TextureType.DirtTwo, new Texture(texturesPath + "tiles/dirt2.png"));
        loadedTextures.Add(TextureType.Bed, new Texture(texturesPath + "structures/bed.png"));
        loadedTextures.Add(TextureType.Crosshair, new Texture(texturesPath + "ui/crosshair.png"));
        loadedTextures.Add(TextureType.CloseIcon, new Texture(texturesPath + "ui/closeWindowIcon.png"));
        loadedTextures.Add(TextureType.Copper, new Texture(texturesPath + "tiles/copper.png"));
        loadedTextures.Add(TextureType.Iron, new Texture(texturesPath + "tiles/iron.png"));
        loadedTextures.Add(TextureType.Colonist, new Texture(texturesPath + "colonists/colonist.png"));
        loadedTextures.Add(TextureType.DefaultTexture, new Texture(texturesPath + "defaultTexture.png"));
        loadedTextures.Add(TextureType.Chest, new Texture(texturesPath + "structures/chest.png"));
        loadedTextures.Add(TextureType.BuildButton, new Texture(texturesPath + "ui/buildButton.png"));
        

        // fonts
        fonts.Add("default", new Font(fontsPath + "default.ttf"));
    }

    public static Font fetchFont(string fontName)
    {
        if (!fonts.ContainsKey(fontName))
        {
            Log.Error($"{fontName} does not exist.");
            return fonts["default"];
        }
        return fonts[fontName];
    }

    public static Texture fetchTexture(TextureType type)
    {
        if (!loadedTextures.ContainsKey(type))
        {
            Log.Error($"{type} does not exist.");
            return loadedTextures[TextureType.DefaultTexture];
        }
        return loadedTextures[type];
    }
}