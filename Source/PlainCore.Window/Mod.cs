using System;

namespace PlainCore.Window
{
    /// <summary>
    /// Enum for modifiers pressed when receiving key or mouse events
    /// </summary>
    [Flags]
    public enum Mod: byte
    {
        None = 0,
        Shift = 1 << 0,
        Control = 1 << 1,
        Alt = 1 << 2,
        Super = 1 << 3
    }
}
