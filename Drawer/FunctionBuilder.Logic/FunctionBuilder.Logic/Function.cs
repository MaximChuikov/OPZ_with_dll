using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FunctionBuilder.Logic
{
    public class Function
    {
        private List<string> output = new List<string>();
        private int i = 0;
        public string Rpn { get; }
        public Function(string expression)
        {
            Iteration(expression);
        }
        void Iteration(string exp)
        {
            var stack = new Stack<Operation>();
            int lastPriority;
            bool flag = false;
            for (; i < exp.Length; i++)
            {
                if (stack.Count != 0)
                    lastPriority = stack.Peek().Priority;
                else
                    lastPriority = 0;
                if (char.IsWhiteSpace(exp[i]))
                    continue;
                else if (char.IsDigit(exp[i]))
                {
                    output.Add(ReadDigit(exp).ToString());
                }
                else
                {
                    switch (exp[i])
                    {
                        case '+':
                            stack.Push(new Plus());
                            break;
                        case '-':
                            stack.Push(new Minus());
                            break;
                        case '*':
                            stack.Push(new Multiply());
                            break;
                        case '/':
                            stack.Push(new Divide());
                            break;
                        case '(':
                            i++;
                            Iteration(exp);
                            break;
                        case ')':
                            output.Add(stack.Pop().ToString());
                            flag = true;
                            break;
                        default:
                            throw new Exception("Неизвестная операция " + exp[i]);
                    }
                }
                if (flag)
                    break;
                if (stack.Count >= 2)
                    if (lastPriority >= stack.Peek().Priority)
                        output.Add(stack.Pop().ToString());
            }
            if (stack.Count != 0)
                output.Add(stack.Pop().ToString());
        }
        int ReadDigit(string exp)
        {
            var number = new StringBuilder();
            while (char.IsDigit(exp[i]))
            {
                number.Append(exp[i]);
                i++;
                if (i == exp.Length)
                    break;
            }
            i--;
            try
            {
                return Int32.Parse(number.ToString());
            }
            catch
            {
                throw new Exception("\nInvalid number! " + number);
            }
        }
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (string str in output)
            {
                result.Append(str + " ");
            }
            return result.ToString();
        }

    }
}
