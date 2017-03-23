using Microsoft.VisualStudio.TestTools.UnitTesting;
using AsmGrammar;

namespace AsmUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void CannotParseString()
        {
            Asm asm = new Asm();
            asm.Evaluate(@"Джигурда");
        }

        [TestMethod]
        [ExpectedException(typeof(AddressException))]
        public void AddressException()
        {
            Asm asm = new Asm();
            asm.Evaluate(@"ld r20, #15");
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterOutOfRangeException))]
        public void ParameterOutOfRangeException()
        {
            Asm asm = new Asm();
            asm.Evaluate(@"ld r2, #256");
        }

        [TestMethod]
        [ExpectedException(typeof(LabelUnavailableException))]
        public void LabelDoesNotExist()
        {
            Asm asm = new Asm();
            asm.Evaluate(@"
ld r10, #12
ld r0, #1
mov r1, r10
m1:
sub r1, r0
brgz metka, r1
");
        }

        [TestMethod]
        public void CalculationVerify()
        {
            Asm asm = new Asm();
            string ans = asm.Evaluate(@"
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
            string answers = "\nr0 = 1\nr1 = 0\nr10 = 12\nr14 = 66";
            Assert.AreEqual(ans, answers);
        }
    }
}
