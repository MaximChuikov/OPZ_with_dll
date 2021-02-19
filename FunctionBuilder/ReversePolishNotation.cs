using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
    class ReversePolishNotation
    {
        private List<object> output = new List<object>();
        private int i = 0;

        public ReversePolishNotation(string exp)
        {
            Iteration(exp);
        }
        public ReversePolishNotation(string exp, ref object[] array)
        {
            Iteration(exp);
            array = output.ToArray();
        }

        void Iteration(string exp)
        {
            var stack = new Stack<Operation>();
            int lastPriority;
            bool flag = false;
            for (; i < exp.Length; i++)
            {
                if (char.IsWhiteSpace(exp[i]))                  //пропуск пробелов
                    continue;

                if (stack.Count != 0)                           //сохр. last знач. стека
                    lastPriority = stack.Peek().Priority;
                else
                    lastPriority = 0;

                if (char.IsDigit(exp[i]))                       //считывание цифры
                {
                    output.Add(ReadDigit(exp));
                    continue;
                }
                else                                            //считывание операции
                {
                    if (!AddOperationToStack(ref stack, exp[i]))
                        switch (exp[i])
                        {
                            case '(':
                                i++;
                                Iteration(exp);
                                continue;
                            case ')':
                                ToEmptyStack(ref stack);
                                flag = true;
                                break;
                            default:
                                throw new Exception("Unkown operation!");
                        }
                    if (stack.Count >= 2)                           // x + x / x * x + x-> опустошить стек
                        if (lastPriority >= stack.Peek().Priority)
                            ToEmptyStackWithoutFirst(ref stack, stack.Peek().Priority);
                }

                if (flag)                                           //завершение метода при завершающей скобке
                    break;
            }
            if (stack.Count > 0 && !flag)                           //опустошение локального стека
                ToEmptyStack(ref stack);
        }
        private void ToEmptyStackWithoutFirst(ref Stack<Operation> stack, int priority)
        {
            Operation oper;
            if (stack.Count > 0)
                oper = stack.Pop();
            else
                throw new Exception("Empty stack!");

            while (stack.Count > 0 && stack.Peek().Priority >= priority)
            {
                output.Add(stack.Pop());
            }

            stack.Push(oper);
        }
        private void ToEmptyStack(ref Stack<Operation> stack)
        {
            while (stack.Count > 0)
            {
                output.Add(stack.Pop());
            }
        }
        decimal ReadDigit(string exp)
        {
            var number = new StringBuilder();
            while (char.IsDigit(exp[i]) || exp[i] == ',')
            {
                number.Append(exp[i]);
                i++;

                if (i >= exp.Length)
                    break;
            }

            i--;

            try
            {
                return decimal.Parse(number.ToString());
            }
            catch
            {
                throw new Exception("\nInvalid number! " + number);
            }
        }

        private bool AddOperationToStack(ref Stack<Operation> stack, char operation)
        {
            switch (operation)
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
                default:
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (object str in output)
            {
                result.Append(str.ToString() + " ");
            }
            return result.ToString();
        }
    }
}
