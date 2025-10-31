using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;
using NodeGraph.Core.Executions;
using NodeGraph.Core.Nodes;

namespace NodeGraph.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ex1();
        }

        static void Ex1()
        {
            Console.WriteLine("Ex 1. (x*2)+4");

            Context context = new Context();
            PipelineEvaluator pipe = new PipelineEvaluator();

            pipe.Add(new ConstNode(3, "x", "Set x"));
            pipe.Add(new ConstNode(2, "two", "Set 2"));
            pipe.Add(new MulNode("x", "two", "t1", "x*2"));
            pipe.Add(new ConstNode(4, "four", "Set 4"));
            pipe.Add(new AddNode("t1", "four", "result", "t+4"));

            pipe.Evaluate(context);
            Console.WriteLine(context.Get<double>("result"));
        }
    }
}
