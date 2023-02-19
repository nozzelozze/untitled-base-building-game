using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

public class SoundManager
{
    public enum SoundType
    {
        Colonist
    }

    private static Func<string, Sound> getSound = (soundName) => new Sound(new SoundBuffer("assets/sound/" + soundName + ".wav"));

    private static Dictionary<SoundType, Sound> loadedSounds = new Dictionary<SoundType, Sound>
    {
        {SoundType.Colonist, getSound("colonist")}
    };


    public static void playSFX(SoundType sound, Vector2f ?  soundPosition = null)
    {
        if (!loadedSounds.ContainsKey(sound))
        {
            Log.Error($"{sound} does not exist.");
            return;
        }
        Sound newSound = new Sound(loadedSounds[sound]);
        if (soundPosition != null)
        {
            float x = soundPosition?.X ?? 0.0f;
            float y = soundPosition?.X ?? 0.0f;
            float dist = Math.Clamp(Camera.getDistanceToCamera(new Vector2f(x, y)), 1, 2000f);
            newSound.Volume = ((2000f/dist));
            if (dist != 2000) newSound.Volume *= (1 - Camera.getZoomLevel());
            newSound.Volume = Math.Clamp(newSound.Volume, 0, 120);
        }
        newSound.Play();
    }
}