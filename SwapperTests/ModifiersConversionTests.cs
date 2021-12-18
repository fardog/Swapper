using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swapper;

namespace SwapperTests;

[TestClass]
public class ModifiersConversionTests
{
    [TestMethod]
    public void FromStringTest()
    {
        string modifier = "Ctrl";
        Modifiers expected = Modifiers.Control;
        Modifiers actual = ModifiersConversion.FromString(modifier);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FromStringCaseInsensitive()
    {
        string modifier = "CTRL";
        Modifiers expected = Modifiers.Control;
        Modifiers actual = ModifiersConversion.FromString(modifier);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FromStringOmnibusTest()
    {
        Dictionary<string, Modifiers> cases = new()
        {
            { "Alt", Modifiers.Alt },
            { "Control", Modifiers.Control },
            { "Ctrl", Modifiers.Control },
            { "Shift", Modifiers.Shift },
            { "Windows", Modifiers.Windows },
            { "Win", Modifiers.Windows },
        };

        foreach(var (key, value) in cases)
        {
            Assert.AreEqual(value, ModifiersConversion.FromString(key));
        }
    }
}
