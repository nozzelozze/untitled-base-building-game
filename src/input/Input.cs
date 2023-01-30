using System;
using SFML.Window;

class Input
{
    Dictionary<Mouse.Button, bool> mouseButtons = new Dictionary<Mouse.Button, bool>();
    Dictionary<Keyboard.Key, bool> keyboardButtons = new Dictionary<Keyboard.Key, bool>();
    
    public Input()
    {
        foreach (Mouse.Button mouseButton in Enum.GetValues(typeof(Mouse.Button)))
        {
            mouseButtons.Add(mouseButton, false);
        }
        foreach (Keyboard.Key keyboardButton in Enum.GetValues(typeof(Keyboard.Key)))
        {
            if (!keyboardButtons.ContainsKey(keyboardButton)) keyboardButtons.Add(keyboardButton, false);
        }
    }
    
    public List<object> getEvents()
    {
        var events = new List<object>();

        foreach(KeyValuePair<Mouse.Button, bool> entry in mouseButtons)
        {
            if (Mouse.IsButtonPressed(entry.Key))
            {
                if (!entry.Value)
                {
                    mouseButtons[entry.Key] = true;
                    events.Add(entry.Key);
                }
            } else
            {
                mouseButtons[entry.Key] = false;
            }
        }
        foreach(KeyValuePair<Keyboard.Key, bool> entry in keyboardButtons)
        {
            if (Keyboard.IsKeyPressed(entry.Key))
            {
                if (!entry.Value)
                {
                    keyboardButtons[entry.Key] = true;
                    events.Add(entry.Key);
                }
            } else
            {
                keyboardButtons[entry.Key] = false;
            }
        }
        return events;
    }
}