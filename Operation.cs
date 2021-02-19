using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
    public abstract class Operation
    {
        public abstract string Name { get; }
        public abstract int Priority { get; }
        public abstract int Args { get; }
        public abstract decimal Count(decimal[] args);

        public override string ToString()
        {
            return Name;
        }
    }
    class Plus : Operation
    {
        public override string Name => "+";
        public override int Priority => 1;
        public override int Args => 2;
        public override decimal Count(decimal[] args)
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
        public override int Args => 2;
        public override decimal Count(decimal[] args)
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
        public override int Args => 2;
        public override decimal Count(decimal[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            return args[0] * args[1];
        }
    }
    class Divide : Operation
    {
        public override string Name => "/";
        public override int Priority => 3;
        public override int Args => 2;
        public override decimal Count(decimal[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            if (args[1] == 0)
                throw new Exception("Знаменатель должен быть неравен нулю!");
            return args[0] / args[1];
        }
    }
}
