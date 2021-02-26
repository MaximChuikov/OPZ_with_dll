using System;
namespace FunctionBuilder.Logic
{
    public class Variable
    {
        public Variable(char name, decimal value)
        {
            this.name = name;
            this.value = value;
        }
        public readonly char name;
        public decimal value;

        public override string ToString()
        {
            return name.ToString();
        }
    }
    public class Parenthessis
    {
        public bool IsOpening { get; }

        public Parenthessis(string parenthesis)
        {
            IsOpening = parenthesis == "(";
        }

        public override string ToString()
        {
            return IsOpening ? "(" : ")";
        }
    }
    public abstract class Operation
    {
        public abstract string Name { get; }
        public abstract int Priority { get; }
        public abstract int Args { get; }
        public abstract decimal Count(decimal[] args);
        public abstract bool IsPrefix { get; }
        public abstract bool IsMiddle { get; }
        public abstract bool IsPostfix { get; }
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
        public override bool IsPrefix => false;
        public override bool IsMiddle => true;
        public override bool IsPostfix => false;
        public override decimal Count(decimal[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            return args[0] + args[1];
        }
    }
    class Sin : Operation
    {
        public override string Name => "sin";
        public override int Priority => 5;
        public override int Args => 1;
        public override bool IsPrefix => true;
        public override bool IsMiddle => false;
        public override bool IsPostfix => false;

        public override decimal Count(decimal[] args)
        {
            if (args.Length != 1)
                throw new Exception("Нужен 1 аргумент");
            return (decimal)Math.Sin((double)args[0]);
        }
    }
    class ACos : Operation
    {
        public override string Name => "arccos";
        public override int Priority => 5;
        public override int Args => 1;
        public override bool IsPrefix => true;
        public override bool IsMiddle => false;
        public override bool IsPostfix => false;

        public override decimal Count(decimal[] args)
        {
            if (args.Length != 1)
                throw new Exception("Нужен 1 аргумент");
            return (decimal)Math.Acos((double)args[0]);
        }
    }
    class ASin : Operation
    {
        public override string Name => "arcsin";
        public override int Priority => 5;
        public override int Args => 1;
        public override bool IsPrefix => true;
        public override bool IsMiddle => false;
        public override bool IsPostfix => false;

        public override decimal Count(decimal[] args)
        {
            if (args.Length != 1)
                throw new Exception("Нужен 1 аргумент");
            return (decimal)Math.Asin((double)args[0]);
        }
    }
    class Cos : Operation
    {
        public override string Name => "cos";
        public override int Priority => 5;
        public override int Args => 1;
        public override bool IsPrefix => true;
        public override bool IsMiddle => false;
        public override bool IsPostfix => false;

        public override decimal Count(decimal[] args)
        {
            if (args.Length != 1)
                throw new Exception("Нужен 1 аргумент");
            return (decimal)Math.Cos((double)args[0]);
        }
    }
    class Pow : Operation
    {
        public override string Name => "^";
        public override int Priority => 5;
        public override int Args => 2;
        public override bool IsPrefix => false;
        public override bool IsMiddle => true;
        public override bool IsPostfix => false;

        public override decimal Count(decimal[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            return (decimal)Math.Pow((double)args[0], (double)args[1]);
        }
    }
    class Sqrt : Operation
    {
        public override string Name => "sqrt";
        public override int Priority => 5;
        public override int Args => 1;
        public override bool IsPrefix => true;
        public override bool IsMiddle => false;
        public override bool IsPostfix => false;

        public override decimal Count(decimal[] args)
        {
            if (args.Length != 1)
                throw new Exception("Нужен 1 аргумент");
            if (args[0] < 0)
                throw new Exception("Аргумент должен быть >= 0");
            return (decimal)Math.Sqrt((double)args[0]);
        }
    }
    class Log : Operation
    {
        public override string Name => "log";
        public override int Priority => 5;
        public override int Args => 2;
        public override bool IsPrefix => true;
        public override bool IsMiddle => false;
        public override bool IsPostfix => false;

        public override decimal Count(decimal[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            if (args[0] <= 0 || args[1] <= 0)
                throw new Exception("Аргументы должны быть >0");
            if (args[0] == 1)
                throw new Exception("Основание должно быть неравным 1");
            return (decimal)Math.Log((double)args[1], (double)args[0]);
        }
    }
    class Minus : Operation
    {
        public override string Name => "-";
        public override int Priority => 1;
        public override int Args => 2;
        public override bool IsPrefix => false;
        public override bool IsMiddle => true;
        public override bool IsPostfix => false;

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
        public override bool IsPrefix => false;
        public override bool IsMiddle => true;
        public override bool IsPostfix => false;
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
        public override bool IsPrefix => false;
        public override bool IsMiddle => true;
        public override bool IsPostfix => false;
        public override decimal Count(decimal[] args)
        {
            if (args.Length != 2)
                throw new Exception("Надо 2 аргумента");
            if (args[1] == 0)
                throw new Exception("Знаменатель должен быть неравен нулю");
            return args[0] / args[1];
        }
    }
}
