namespace FunctionBuilder.Logic
{
    public class Function
    {
        public string ReversePolishNotation { get; }
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
        private Variable x = new Variable('x', 0);
        public Function(string expression)
        {
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
