using System.Collections.Generic;
namespace FunctionBuilder.Logic
{
    class ReversePolishNotationSolver
    {
        public decimal answer { get; private set; }
        private object[] array;
        private Variable x;
        public ReversePolishNotationSolver(object[] array, Variable x)
        {
            this.array = array;
            this.x = x;
            Recalculate();
        }
        public void Recalculate()
        {
            decimal num;
            var stack = new Stack<decimal>();
            foreach (var element in array)
            {
                if (decimal.TryParse(element.ToString(), out num))
                    stack.Push(num);
                else if (element is Variable)
                    stack.Push(x.value);
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
        public override string ToString()
        {
            return answer.ToString();
        }
    }
}
