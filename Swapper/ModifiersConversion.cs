using System;
using System.Collections.Generic;

namespace Swapper;

public static class ModifiersConversion
{
    private static readonly Dictionary<string, Modifiers> _modifiers = new()
    {
        { "alt", Modifiers.Alt },
        { "control", Modifiers.Control },
        { "ctrl", Modifiers.Control },
        { "shift", Modifiers.Shift },
        { "windows", Modifiers.Windows },
        { "win", Modifiers.Windows },
    };

    public static Modifiers FromString(string value)
    {
        if (_modifiers.TryGetValue(value.ToLower(), out var m))
            return m;

        throw new ArgumentException($"invalid modifier: {value}");
    }
}
