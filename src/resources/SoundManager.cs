using System;
using SFML.System;
using SFML.Audio;
using System.Collections.Generic;

public class SoundManager
{
    public enum SoundType
    {
        Build
    }

    private static Func<string, Sound> GetSound = (soundName) => new Sound(new SoundBuffer("assets/sound/" + soundName + ".wav"));

    private static Dictionary<SoundType, Sound> LoadedSounds = new Dictionary<SoundType, Sound>
    {
        {SoundType.Build, GetSound("build2")}
    };


    public static void PlaySFX(SoundType sound, Vector2f? soundPosition = null)
    {
        if (!LoadedSounds.ContainsKey(sound))
        {
            Log.Error($"{sound} does not exist.");
            return;
        }
        Sound newSound = new Sound(LoadedSounds[sound]);
        if (soundPosition != null)
        {
            float x = soundPosition?.X ?? 0.0f;
            float y = soundPosition?.X ?? 0.0f;
            float dist = Math.Clamp(Camera.GetDistanceToCamera(new Vector2f(x, y)), 1, 2000f);
            newSound.Volume = ((2000f/dist));
            if (dist != 2000) newSound.Volume *= (1 - Camera.GetZoomLevel());
            newSound.Volume = Math.Clamp(newSound.Volume, 0, 120);
        }
        newSound.Play();
    }
}
