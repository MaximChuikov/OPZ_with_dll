using System;
using FunctionBuilder.Logic;

namespace Fun
{
    
    class Program
    {
        static void Main()
        {
            var f = new Function("log(2^2;sqrt(1024))");
            Console.WriteLine($"ОПЗ = {f.ReversePolishNotation}, решение = {f.Answer}");
            Console.ReadKey();
        }
    }
}
