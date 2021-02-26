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
        [TestCase]
        public void Test()
        {
            var result = new Function("x").Answer; //изначально х = 0
            Assert.AreNotEqual(1m, result);
            Assert.AreEqual(0m, result);
        }

        [TestCase("sin(log(x;sqrt(4)))", ExpectedResult = "x 4 sqrt log sin")]
        [TestCase("1-2/(1/2*(1-2/3*4))", ExpectedResult = "1 2 1 2 1 2 3 4 * / - * / / -")]
        [TestCase("-10000+x", ExpectedResult = "-10000 x +")]
        [TestCase("sqrt((-1*x)^2)", ExpectedResult = "-1 x * 2 ^ sqrt")]
        [TestCase("log(arccos(-1*x)                 ;45)", ExpectedResult = "-1 x * arccos 45 log")]
        public string RPN_Test(string expression)
        {
            return new ReversePolishNotation(new ToTokensTranslator(expression, new Variable('x', 0m)).TokensArray).ToString();
        }
        [TestCase(2, ExpectedResult = 1d)]
        [TestCase(16, ExpectedResult = 2d)]
        [TestCase(1, ExpectedResult = 0d)]
        public double Solver_Test(decimal x)
        {
            var variable = new Variable('x', x);
            var f = new Function("sqrt(log(2;x))", variable);
            return (double)f.Answer;
        }
    }
}