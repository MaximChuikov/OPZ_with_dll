using System;
using FunctionBuilder.Logic;

namespace Fun
{
    
    class Program
    {
        static void Main()
        {
            var f = new Function("13,2-1");
            Console.WriteLine($"ОПЗ = {f.ReversePolishNotation}, решение = {f.Answer}");
            Console.ReadKey();
        }
    }
}
