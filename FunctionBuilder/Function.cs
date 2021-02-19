using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
    public class Function
    {
        public string ReversePolishNotation { get; }
        public decimal Answer { get; }
        private object[] array;
        public Function(string expression)
        {
            ReversePolishNotation = new ReversePolishNotation(expression, ref array).ToString();
            Answer = new ReversePolishNotationSolver(array).GetValue();
        }
    }
}
