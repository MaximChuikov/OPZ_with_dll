using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
    class ToTokensTranslator
    {
        public object[] TokensArray { get; }
        public ToTokensTranslator(string expression, Variable x)
        {
            TokensArray = Translate(expression, x);
        }
        object[] Translate(string exp, Variable x)
        {
            exp = DeleteSpaces(exp);    //очистка от пробелов
            var tokensList = new List<object>();
            int i = 0;
            int len;
            for (; i < exp.Length; i++)
            {
                if (char.IsDigit(exp[i]))
                {
                    if (tokensList.Count == 1 && tokensList[0] is Minus) //если минус стоит первым в строке
                    {
                        ChangeLastElement(ref tokensList, (object)(-1 * ReadDigit(exp, ref i)));
                    }
                    else if (tokensList.Count >= 2)
                    {
                        if (tokensList[tokensList.Count - 1] is Minus) //если дальше первого
                        {
                            len = tokensList.Count;
                            if (tokensList[len - 2] is Operation || tokensList[len - 2] is Parenthessis)
                            {
                                ChangeLastElement(ref tokensList, (object)(-1 * ReadDigit(exp, ref i)));
                            }
                        }
                        else //просто число
                        {
                            tokensList.Add((object)ReadDigit(exp, ref i));
                        }
                    }
                }
                else if (exp[i] == x.name) //переменная
                    tokensList.Add((object)x);
                else if (exp[i] == ';') //вместо (;) разделяю на ()() 
                {
                    tokensList.Add(new Parenthessis(")"));
                    tokensList.Add(new Parenthessis("("));
                }
                else
                {
                    tokensList.Add(DefineOperation(ReadOperation(exp, ref i))); //добавить операцию
                }
            }
            return tokensList.ToArray();
        }
        void ChangeLastElement(ref List<object> tokens, object element)
        {
            if (tokens.Count >= 1)
            {
                tokens.RemoveAt(tokens.Count - 1);
                tokens.Add(element);
            }
        }
        string DeleteSpaces(string exp)
        {
            var str = new StringBuilder();
            foreach(var a in exp)
            {
                if (!char.IsWhiteSpace(a))
                    str.Append(a);
            }
            return str.ToString();
        }
        decimal ReadDigit(string exp, ref int i) //считывает число и останавливается на последней цифре числа
        {
            var number = new StringBuilder();
            while (char.IsDigit(exp[i]) || exp[i] == ',')
            {
                number.Append(exp[i]);
                i++;
                if (i >= exp.Length)
                    break;
            }
            i--;//
            try   
            { 
                return decimal.Parse(number.ToString()); 
            }
            catch 
            { 
                throw new Exception("Invalid number! " + number); 
            }
        }
        string ReadOperation(string exp, ref int i) //заканчивает работу на последнем символе операции
        {
            var operation = new StringBuilder();
            while(!char.IsDigit(exp[i]))
            {
                operation.Append(exp[i]);

                try
                {
                    DefineOperation(operation.ToString());
                    break;
                }
                catch
                {
                    i++;
                }
            }
            return operation.ToString();
        }
        object DefineOperation(string operation)
        {
            switch(operation)
            {
                case "+":
                    return new Plus();
                case "-":
                    return new Minus();
                case "*":
                    return new Multiply();
                case "/":
                    return new Divide();
                case "log":
                    return new Log();
                case "sqrt":
                    return new Sqrt();
                case "^":
                    return new Pow();
                case "sin":
                    return new Sin();
                case "cos":
                    return new Cos();
                case "arccos":
                    return new ACos();
                case "arcsin":
                    return new ASin();
                case "(":
                    return new Parenthessis(operation);
                case ")":
                    return new Parenthessis(operation);
                default:
                    throw new Exception("Unkown operation" + operation);
            }
        }
    }
}
