using System.Collections.Generic;
namespace FunctionBuilder.Logic
{
    class ReversePolishNotationSolver
    {
        private decimal answer;
        public ReversePolishNotationSolver(object[] array)
        {
            decimal num;
            var stack = new Stack<decimal>();
            foreach(var element in array)
            {
                if (decimal.TryParse(element.ToString(), out num))
                    stack.Push(num);
                else
                    DoCount(ref stack, (Operation)element);
            }
            answer = stack.Pop();
        }
        private void DoCount(ref Stack<decimal> stack, Operation operation)
        {
            var args = new decimal[operation.Args];
            for (int i = args.Length - 1; i >= 0; i--)
            {
                args[i] = stack.Pop();
            }
            stack.Push(operation.Count(args));
        }
        public decimal GetValue()
        {
            return answer;
        }
        public override string ToString()
        {
            return answer.ToString();
        }
    }
}
