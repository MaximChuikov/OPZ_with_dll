using System;
using FunctionBuilder.Logic;

namespace Fun
{
    
    class Program
    {
        static void Main()
        {
            var f = new Function("sqrt(1+(-6+3^2)+0)");
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
