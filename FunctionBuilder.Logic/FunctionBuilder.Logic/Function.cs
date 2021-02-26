namespace FunctionBuilder.Logic
{
    public class Function
    {
        public string ReversePolishNotation { get; } // 1 -6 3 2 ^ + + 0 +
        public decimal Answer
        {
            get
            {
                return solver.answer;
            }
        }
        public decimal X
        {
            get
            {
                return x.value;
            }
            set
            {
                x.value = (decimal)value;
                solver.Recalculate();
            }
        }
        private ReversePolishNotationSolver solver;
        private Variable x;
        public Function(string expression)
        {
            x = new Variable('x', 0);
            object[] array;
            ReversePolishNotation = new ReversePolishNotation(new ToTokensTranslator(expression, x).TokensArray, out array).ToString();
            solver = new ReversePolishNotationSolver(array, x);
        }
        public Function(string expression, Variable var)
        {
            x = new Variable(var.name, var.value);
            object[] array;
            ReversePolishNotation = new ReversePolishNotation(new ToTokensTranslator(expression, x).TokensArray, out array).ToString();
            solver = new ReversePolishNotationSolver(array, x);
        }
        public override string ToString()
        {
            return ReversePolishNotation;
        }
    }
}
