using System;

namespace Swapper;

[Flags]
public enum Modifiers
{
    Alt = 0x0001,
    Control = 0x0002,
    Shift = 0x0004,
    Windows = 0x0008,
}
