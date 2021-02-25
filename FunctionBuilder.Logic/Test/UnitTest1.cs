using NUnit.Framework;
using FunctionBuilder.Logic;

namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase]
        public void TestRPN()
        {
            var result = new Function("log(2;8^2)").ReversePolishNotation;
            Assert.AreEqual("2 8 2 ^ log", result);
        }
        [TestCase]
        public void Test_RPN_and_Solver()
        {
            var result = new Function("sqrt(1+(-6+3^2)+0)").Answer;
            Assert.AreEqual(2m, result);
        }
        [TestCase]
        public void Test_Variable()
        {
            var result = new Function("1-1+(-10+4*x)^2-(16*x^2-80*x+100)");
            result.X = 999m;
            Assert.AreEqual(0m, result.Answer);
            result.X = -13m;
            Assert.AreEqual(0m, result.Answer);
            result.X = 1111.234m;
            Assert.AreEqual(0m, result.Answer);
        }
    }
}