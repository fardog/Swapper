using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Swapper.Tests
{
    [TestClass()]
    public class ModifiersConversionTests
    {
        [TestMethod()]
        public void FromStringTest()
        {
            string modifierRepr = "Ctrl";
            Modifiers expected = Modifiers.Control;
            Modifiers actual = ModifiersConversion.FromString(modifierRepr);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FromStringCaseInsensitive()
        {
            string modifierRepr = "CTRL";
            Modifiers expected = Modifiers.Control;
            Modifiers actual = ModifiersConversion.FromString(modifierRepr);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
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

            foreach(var entry in cases)
            {
                Assert.AreEqual(entry.Value, ModifiersConversion.FromString(entry.Key));
            }
        }
    }
}
