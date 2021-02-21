namespace FunctionBuilder.Logic
{
    public class Function
    {
        public string ReversePolishNotation { get; }
        public decimal Answer { get; }
        private object[] array;
        public Function(string expression)
        {
            ReversePolishNotation = new ReversePolishNotation(new ToTokensTranslator(expression).TokensArray, ref array).ToString();
            Answer = new ReversePolishNotationSolver(array).GetValue();
        }
    }
}
