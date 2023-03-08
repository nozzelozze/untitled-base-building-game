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
        Icon,
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
        loadedTextures.Add(TextureType.DirtOne, new Texture(texturesPath + "dirt1.png"));
        loadedTextures.Add(TextureType.DirtTwo, new Texture(texturesPath + "dirt2.png"));
        loadedTextures.Add(TextureType.Bed, new Texture(texturesPath + "bed.png"));
        loadedTextures.Add(TextureType.Crosshair, new Texture(texturesPath + "crosshair.png"));
        loadedTextures.Add(TextureType.Icon, new Texture(getTexturePath("icon")));
        loadedTextures.Add(TextureType.CloseIcon, new Texture(getTexturePath("closeWindowIcon")));
        loadedTextures.Add(TextureType.Copper, new Texture(getTexturePath("copper")));
        loadedTextures.Add(TextureType.Iron, new Texture(getTexturePath("iron")));
        loadedTextures.Add(TextureType.Colonist, new Texture(getTexturePath("colonist")));
        loadedTextures.Add(TextureType.DefaultTexture, new Texture(getTexturePath("defaultTexture")));
        loadedTextures.Add(TextureType.Chest, new Texture(getTexturePath("chest")));
        loadedTextures.Add(TextureType.BuildButton, new Texture(getTexturePath("buildButton")));
        

        // fonts
        fonts.Add("default", new Font(fontsPath + "default.ttf"));
    }

    public Func<string, string> getTexturePath = (name) => texturesPath + name + ".png";

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