using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swapper;

namespace SwapperTests
{
    [TestClass()]
    public class HotKeyTests
    {
        [TestMethod()]
        public void HotKeyConstructorTest()
        {
            Modifiers modifiers = Modifiers.Alt | Modifiers.Control;
            Keys key = Keys.Left;
            HotKey hotKey = new(modifiers, key);

            Assert.AreEqual(modifiers, hotKey.Modifiers);
            Assert.AreEqual(key, hotKey.Key);
        }

        [TestMethod()]
        public void HotKeyStringConstructorTest()
        {
            Modifiers modifiers = Modifiers.Alt | Modifiers.Control;
            Keys key = Keys.Left;
            HotKey expected = new(modifiers, key);
            HotKey actual = new("Control+Alt+Left");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HotKeyStringConstructorFailsWithoutModifiersTest()
        {
            Assert.ThrowsException<ArgumentException>(() => new HotKey("Left"));
        }

        [TestMethod()]
        public void HotKeyStringConstructorFailsWithoutKeyTest()
        {
            Assert.ThrowsException<ArgumentException>(() => new HotKey("Control+Alt"));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Dictionary<string, HotKey> cases = new()
            {
                { "Win+Ctrl+K", new HotKey(Modifiers.Windows | Modifiers.Control, Keys.K) },
                { "Ctrl+Alt+K", new HotKey(Modifiers.Alt | Modifiers.Control, Keys.K) },
                { "Ctrl+Left", new HotKey(Modifiers.Control, Keys.Left) },
            };
            foreach (var entry in cases)
            {
                Assert.AreEqual(entry.Key, entry.Value.ToString());
            }
        }

        [TestMethod()]
        public void EqualsTest()
        {
            HotKey hotKey1 = new(Modifiers.Control | Modifiers.Alt, Keys.Left);
            HotKey hotKey2 = new(Modifiers.Control | Modifiers.Alt, Keys.Left);
            HotKey hotKey3 = new(Modifiers.Control | Modifiers.Shift, Keys.Left);
            HotKey hotKey4 = new(Modifiers.Control | Modifiers.Alt, Keys.Right);

            Assert.IsTrue(hotKey1 == hotKey2);
            Assert.IsFalse(hotKey1 != hotKey2);
            Assert.IsTrue(hotKey1.Equals(hotKey2));

            Assert.IsFalse(hotKey2 == hotKey3);
            Assert.IsTrue(hotKey2 != hotKey3);
            Assert.IsFalse(hotKey2.Equals(hotKey3));

            Assert.IsFalse(hotKey2 == hotKey4);
            Assert.IsTrue(hotKey2 != hotKey4);
            Assert.IsFalse(hotKey2.Equals(hotKey4));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            HotKey hotKey1 = new(Modifiers.Control | Modifiers.Alt, Keys.Left);
            HotKey hotKey2 = new(Modifiers.Control | Modifiers.Alt, Keys.Left);
            HotKey hotKey3 = new(Modifiers.Control | Modifiers.Shift, Keys.Left);

            Assert.AreEqual(hotKey1.GetHashCode(), hotKey2.GetHashCode());
            Assert.AreNotEqual(hotKey1.GetHashCode(), hotKey3.GetHashCode());
        }
    }
}
