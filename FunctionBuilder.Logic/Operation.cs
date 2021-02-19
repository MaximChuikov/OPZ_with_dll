using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
    class Parenthesis
    {
        public readonly bool isOpening;
        public Parenthesis(char bracket)
        {
            isOpening = bracket == '(';
        }
        public override string ToString()
        {
            return isOpening ? "(" : ")";
        }
    }
    public abstract class Operation
    {
        public abstract string Name { get; }
        public abstract int Priority { get; } // 1(+ -) 2(/) 3(*)

        public abstract double Count(double[] args);

        public override string ToString()
        {
            return Name;
        }
    }
    class Plus : Operation
    {
        public override string Name => "+";
        public override int Priority => 1;

        public override double Count(double[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            return args[0] + args[1];
        }
    }
    class Minus : Operation
    {
        public override string Name => "-";
        public override int Priority => 1;

        public override double Count(double[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            return args[0] - args[1];
        }
    }
    class Multiply : Operation
    {
        public override string Name => "*";
        public override int Priority => 4;

        public override double Count(double[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            return args[0] * args[1];
        }
    }
    class Divide : Operation
    {
        public override string Name => "/";
        public override int Priority => 1;

        public override double Count(double[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            return args[0] + args[1];
        }
    }
}
