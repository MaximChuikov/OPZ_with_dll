using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
    class ToTokensTranslator
    {
        public object[] TokensArray { get; }
        public ToTokensTranslator(string expression)
        {
            TokensArray = Translate(expression);
        }
        object[] Translate(string exp)
        {
            exp = DeleteSpaces(exp);    //очистка от пробелов
            var tokensList = new List<object>();
            int i = 0;
            for (; i < exp.Length; i++)
            {
                if (char.IsDigit(exp[i]))
                {
                    tokensList.Add((object)ReadDigit(exp, ref i));
                }
                else if (exp[i] == ';')
                {
                    tokensList.Add(new Parenthessis(")"));
                    tokensList.Add(new Parenthessis("("));
                }
                else
                {
                    tokensList.Add(DefineOperation(ReadOperation(exp, ref i)));
                }
            }
            return tokensList.ToArray();
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
                case "(":
                    return new Parenthessis(operation);
                case ")":
                    return new Parenthessis(operation);
                default:
                    throw new Exception("Unkown operation" + operation);
            }
        }
        void FindErrors(object[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++) //проверка места операций
            {
                if (tokens[i] is decimal)
                    continue;
                if (tokens[i] is Operation operation)
                    if(operation.IsMiddle)
                    {

                    }


            }
        }

    }
}
