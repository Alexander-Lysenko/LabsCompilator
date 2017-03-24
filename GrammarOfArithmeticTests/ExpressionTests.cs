using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrammarOfArithmetic;

namespace GrammarOfArithmetic.Tests
{
    [TestClass()]
    public class ExpressionTests
    {
        [TestMethod()]
        public void ParseTest1()
        {
            ExpressionModule ex;
            ex = new ExpressionModule("(50)");
            Assert.AreEqual(ex.Parse(), 50);
            ex = new ExpressionModule("(+50)");
            Assert.AreEqual(ex.Parse(), 50);
            ex = new ExpressionModule("(-50)");
            Assert.AreEqual(ex.Parse(), -50);

        }

        [TestMethod()]
        public void ParseTest2()
        {
            ExpressionModule ex;
            ex = new ExpressionModule("2+3");
            Assert.AreEqual(ex.Parse(), 5);
            ex = new ExpressionModule("3*3+3");
            Assert.AreEqual(ex.Parse(), 12);
            ex = new ExpressionModule("(2*2)+2");
            Assert.AreEqual(ex.Parse(), 6);
            ex = new ExpressionModule("(2+2)*2");
            Assert.AreEqual(ex.Parse(), 8);
        }

        [TestMethod()]
        public void ParseTest3()
        {
            ExpressionModule ex;
            ex = new ExpressionModule("2+-3");
            Assert.AreEqual(ex.Parse(), -1);
            ex = new ExpressionModule("3*3--3");
            Assert.AreEqual(ex.Parse(), 12);
            ex = new ExpressionModule("(2*-2)+2");
            Assert.AreEqual(ex.Parse(), -2);
            ex = new ExpressionModule("-(2+2)*2");
            Assert.AreEqual(ex.Parse(), -8);
        }


    }

}