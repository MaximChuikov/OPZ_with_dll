using System;
using FunctionBuilder.Logic;

namespace Fun
{
    
    class Program
    {
        static void Main()
        {
            var f = new Function("log((-2*x)^2+1,1;x^4+sqrt((sin(x)+arccos(x/10))^2)");
            Console.WriteLine($"ОПЗ = {f.ReversePolishNotation}");
            for (decimal i = 1; i < 10; i += (decimal)0.5)
            {
                f.X = i;
                Console.WriteLine($"при х = {f.X}, ответ = {f.Answer}");
            }
            Console.ReadKey();
        }
    }
}
