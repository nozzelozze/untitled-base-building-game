using System;
using SFML.Window;

class Input
{
    Dictionary<Mouse.Button, bool> MouseButtons = new Dictionary<Mouse.Button, bool>();
    Dictionary<Keyboard.Key, bool> KeyboardButtons = new Dictionary<Keyboard.Key, bool>();
    
    public static List<object> Events = new List<object>();

    public Input()
    {
        foreach (Mouse.Button mouseButton in Enum.GetValues(typeof(Mouse.Button)))
        {
            MouseButtons.Add(mouseButton, false);
        }
        foreach (Keyboard.Key keyboardButton in Enum.GetValues(typeof(Keyboard.Key)))
        {
            if (!KeyboardButtons.ContainsKey(keyboardButton)) KeyboardButtons.Add(keyboardButton, false);
        }
    }
    
    public void GetEvents()
    {
        PlayerMouse.OnUI = false;
        Events.Clear();
        foreach(KeyValuePair<Mouse.Button, bool> entry in MouseButtons)
        {
            if (Mouse.IsButtonPressed(entry.Key))
            {
                if (!entry.Value)
                {
                    MouseButtons[entry.Key] = true;
                    Events.Add(entry.Key);
                }
            } else
            {
                MouseButtons[entry.Key] = false;
            }
        }
        foreach(KeyValuePair<Keyboard.Key, bool> entry in KeyboardButtons)
        {
            if (Keyboard.IsKeyPressed(entry.Key))
            {
                if (!entry.Value)
                {
                    KeyboardButtons[entry.Key] = true;
                    Events.Add(entry.Key);
                }
            } else
            {
                KeyboardButtons[entry.Key] = false;
            }
        }
    }
}
