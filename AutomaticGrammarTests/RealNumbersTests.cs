using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticGrammar.Tests
{
    [TestClass()]
    public class RealNumbersTests
    {
        [TestMethod()]
        public void VetifyTest()
        {
            RealNumbers r = new RealNumbers();
            string s = "123";
            Assert.AreEqual(r.Vetify(s), true);
            s = "+123";
            Assert.AreEqual(r.Vetify(s), true);
            s = "-123";
            Assert.AreEqual(r.Vetify(s), true);
            s = "lol";
            Assert.AreEqual(r.Vetify(s), false);
            s = "-123.123e+123";
            Assert.AreEqual(r.Vetify(s), true);
        }
    }
}