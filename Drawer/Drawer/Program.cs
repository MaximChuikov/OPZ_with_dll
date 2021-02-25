using System;
using FunctionBuilder.Logic;

namespace Fun
{
    
    class Program
    {
        static void Main()
        {
            var f = new Function("(-10+4*x)^2-(16*x^2-80*x+100)");
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
