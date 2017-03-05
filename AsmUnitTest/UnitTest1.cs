using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AsmGrammar;

namespace AsmUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CannotParseString()
        {
            Asm asm = new Asm();
            try
            {
                asm.Evaluate(@"
ld r1, #16
mov r1, r10
add r10, r1
sub r1, r1
syscall 10
");
            }
            catch (ParserException e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LabelDoesNotExist()
        {
            Asm asm = new Asm();
            try
            {
                asm.Evaluate(@"
ld r10, #12
ld r0, #1
mov r1, r10
metka:
sub r1, r0
brgz metka, r1
");
            }
            catch (LabelUnavailableException e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CalculationVerify()
        {
            string ans = "";
            Asm asm = new Asm();
            try
            {
                ans = asm.Evaluate(@"
ld r10, #12
ld r0, #1
ld r14, #66
mov r1, r10
metka:
sub r1, r0
br kek_r14
ld r14, #128
add r14, 10

kek_r14:
mov r2, r1
brgz metka, r1

syscall 0
syscall 1
syscall 10
syscall 14 
");
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            string answers = "\nr0 = 1\nr1 = 0\nr10 = 12\nr14 = 66";
            Assert.AreEqual(ans, answers);
        }
    }
}
