using System;
using SFML.Graphics;
using System.Collections.Generic;

public class ResourceLoader
{

    private static string TexturesPath = "assets/imgs/";
    private static string FontsPath = "assets/fonts/";

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
    
    private static Dictionary<TextureType, Texture> LoadedTextures = new Dictionary<TextureType, Texture>();

    private static Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

    public ResourceLoader()
    {
        // textures
        LoadedTextures.Add(TextureType.DirtOne, new Texture(TexturesPath + "tiles/dirt1.png"));
        LoadedTextures.Add(TextureType.DirtTwo, new Texture(TexturesPath + "tiles/dirt2.png"));
        LoadedTextures.Add(TextureType.Bed, new Texture(TexturesPath + "structures/bed.png"));
        LoadedTextures.Add(TextureType.Crosshair, new Texture(TexturesPath + "ui/crosshair.png"));
        LoadedTextures.Add(TextureType.CloseIcon, new Texture(TexturesPath + "ui/closeWindowIcon.png"));
        LoadedTextures.Add(TextureType.Copper, new Texture(TexturesPath + "tiles/copper.png"));
        LoadedTextures.Add(TextureType.Iron, new Texture(TexturesPath + "tiles/iron.png"));
        LoadedTextures.Add(TextureType.Colonist, new Texture(TexturesPath + "colonists/colonist.png"));
        LoadedTextures.Add(TextureType.DefaultTexture, new Texture(TexturesPath + "defaultTexture.png"));
        LoadedTextures.Add(TextureType.Chest, new Texture(TexturesPath + "structures/chest.png"));
        LoadedTextures.Add(TextureType.BuildButton, new Texture(TexturesPath + "ui/buildButton.png"));
        

        // fonts
        Fonts.Add("default", new Font(FontsPath + "default.ttf"));
    }

    public static Font FetchFont(string fontName)
    {
        if (!Fonts.ContainsKey(fontName))
        {
            Log.Error($"{fontName} does not exist.");
            return Fonts["default"];
        }
        return Fonts[fontName];
    }

    public static Texture FetchTexture(TextureType type)
    {
        if (!LoadedTextures.ContainsKey(type))
        {
            Log.Error($"{type} does not exist.");
            return LoadedTextures[TextureType.DefaultTexture];
        }
        return LoadedTextures[type];
    }
}
