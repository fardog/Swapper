using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Swapper
{
    public class HotKey
    {
        public readonly Modifiers Modifiers;
        public readonly Keys Key;

        public HotKey(Modifiers modifiers, Keys key)
        {
            Modifiers = modifiers;
            Key = key;
        }

        public HotKey(string spec)
        {
            Modifiers modifiers = 0;
            Keys key = 0;

            var parts = spec.Split("+").Select(s => s.Trim());
            foreach (var part in parts)
            {
                try
                {
                    modifiers |= ModifiersConversion.FromString(part);
                    continue;
                }
                catch (ArgumentException) { }

                foreach (Keys k in Enum.GetValues(typeof(Keys)))
                    if (k.ToString().ToLower() == part.ToLower())
                        key = k;
            }

            if (modifiers == 0)
                throw new ArgumentException("no modifiers present in spec");
            Modifiers = modifiers;

            if (key == 0)
                throw new ArgumentException("no key present in spec");
            Key = key;
        }

        public override string ToString()
        {
            List<string> parts = new();
            if (Modifiers.HasFlag(Modifiers.Windows))
                parts.Add("Win");
            if (Modifiers.HasFlag(Modifiers.Control))
                parts.Add("Ctrl");
            if (Modifiers.HasFlag(Modifiers.Alt))
                parts.Add("Alt");
            if (Modifiers.HasFlag(Modifiers.Shift))
                parts.Add("Shift");

            parts.Add(Key.ToString());

            return string.Join("+", parts);
        }

        public override bool Equals(object? obj)
        {
            if (obj is HotKey other)
            {
                return other.Key == Key && other.Modifiers == Modifiers;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Modifiers, Key);
        }

        public static bool operator ==(HotKey? a, HotKey? b)
        {
            return a?.Equals(b) ?? false;
        }

        public static bool operator !=(HotKey? a, HotKey? b)
        {
            return !a?.Equals(b) ?? false;
        }
    }
}
