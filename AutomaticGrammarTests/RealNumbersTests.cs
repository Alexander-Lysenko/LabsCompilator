using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticGrammar.Tests
{
    [TestClass()]
    public class RealNumbersTests
    {
        [TestMethod()]
        public void LolTest()
        {
            RealNumbers r = new RealNumbers();
            string s = "123";
            Assert.AreEqual(r.Lol(s), true);
            s = "+123";
            Assert.AreEqual(r.Lol(s), true);
            s = "-123";
            Assert.AreEqual(r.Lol(s), true);
            s = "12lol";
            Assert.AreEqual(r.Lol(s), false);
        }
    }
}