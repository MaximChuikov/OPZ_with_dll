using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
    class ReversePolishNotation
    {
        private List<object> output = new List<object>();
        private int i = 0;
        public ReversePolishNotation(object[] tokens, out object[] array)
        {
            Iteration(tokens);
            array = output.ToArray();
        }
        void Iteration(object[] tokens)
        {
            var stack = new Stack<Operation>();
            int lastPriority;
            for (; i < tokens.Length; i++)
            {
                if (stack.Count != 0)
                    lastPriority = stack.Peek().Priority;
                else
                    lastPriority = 0;

                if (tokens[i] is decimal || tokens[i] is Variable)
                {
                    output.Add(tokens[i]);
                    continue;
                }

                if (tokens[i] is Parenthessis parenthess)
                    if (parenthess.IsOpening)
                    {
                        i++;
                        Iteration(tokens);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                else if (tokens[i] is Operation operation)
                {
                    if (operation.IsPrefix)
                    {
                        for(int j = 0; j < operation.Args; j++)
                        {
                            i += 2;    //пропуск скобок )(
                            Iteration(tokens);
                        }
                        output.Add(operation);
                    }
                    else if (operation.IsMiddle)
                        stack.Push((Operation)tokens[i]);
                }

                if (stack.Count >= 2)
                    if (lastPriority >= stack.Peek().Priority)
                        ToEmptyStackNoFirstToLesserPriority(ref stack, stack.Peek().Priority);
            }
            if (stack.Count > 0)
                ToEmptyStack(ref stack);
        }
        private void ToEmptyStackNoFirstToLesserPriority(ref Stack<Operation> stack, int priority)
        {
            Operation temp;
            if (stack.Count > 0)
                temp = stack.Pop();
            else
                throw new Exception("Empty stack!");

            while (stack.Count > 0 && stack.Peek().Priority >= priority)
            {
                output.Add(stack.Pop());
            }

            stack.Push(temp);
        }
        private void ToEmptyStack(ref Stack<Operation> stack)
        {
            while (stack.Count > 0)
            {
                output.Add(stack.Pop());
            }
        }
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (object str in output)
            {
                result.Append(str.ToString() + " ");
            }
            if (result.Length != 0)
                result.Remove(result.Length - 1, 1);
            return result.ToString();
        }
    }
}
