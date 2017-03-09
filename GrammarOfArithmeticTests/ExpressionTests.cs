using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarOfArithmetic.Tests
{
    [TestClass()]
    public class ExpressionTests
    {
        [TestMethod()]
        public void ParseTest1()
        {
            Expression ex;
            ex = new Expression("(50)");
            Assert.AreEqual(ex.Parse(), 50);
            ex = new Expression("(+50)");
            Assert.AreEqual(ex.Parse(), 50);
            ex = new Expression("(-50)");
            Assert.AreEqual(ex.Parse(), -50);

        }

        [TestMethod()]
        public void ParseTest2()
        {
            Expression ex;
            ex = new Expression("2+3");
            Assert.AreEqual(ex.Parse(), 5);
            ex = new Expression("3*3+3");
            Assert.AreEqual(ex.Parse(), 12);
            ex = new Expression("(2*2)+2");
            Assert.AreEqual(ex.Parse(), 6);
            ex = new Expression("(2+2)*2");
            Assert.AreEqual(ex.Parse(), 8);
        }

        [TestMethod()]
        public void ParseTest3()
        {
            Expression ex;
            ex = new Expression("2+-3");
            Assert.AreEqual(ex.Parse(), -1);
            ex = new Expression("3*3--3");
            Assert.AreEqual(ex.Parse(), 12);
            ex = new Expression("(2*-2)+2");
            Assert.AreEqual(ex.Parse(), -2);
            ex = new Expression("-(2+2)*2");
            Assert.AreEqual(ex.Parse(), -8);
        }


    }

}