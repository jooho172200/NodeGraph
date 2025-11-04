using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;
using NodeGraph.Core.Executions;
using NodeGraph.Core.Graphs;
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

            Graph g = new Graph();

            // 노드 등록 (기존 INode 그대로, 키 규칙만 통일)
            g.AddNode("ConstX", new ConstNode(3.0, Graph.Key("ConstX", "Out"), "Set x"));
            g.AddNode("Const2", new ConstNode(2.0, Graph.Key("Const2", "Out"), "Set 2"));
            g.AddNode("Mul1", new MulNode(
                Graph.Key("Mul1", "A"), Graph.Key("Mul1", "B"), Graph.Key("Mul1", "Out"), "x*2"));
            g.AddNode("Const4", new ConstNode(4.0, Graph.Key("Const4", "Out"), "Set 4"));
            g.AddNode("Add1", new AddNode(
                Graph.Key("Add1", "A"), Graph.Key("Add1", "B"), Graph.Key("Add1", "Out"), "t+4"));

            // 엣지 연결 (등록/파일상 순서와 무관)
            
            g.Connect("Const2", "Out", "Mul1", "B");
            
            g.Connect("Const4", "Out", "Add1", "B");
            g.Connect("Mul1", "Out", "Add1", "A");
            g.Connect("ConstX", "Out", "Mul1", "A");

            Context context = new Context();


            PipelineEvaluator pipe = new PipelineEvaluator(g);

            pipe.Evaluate(context);

            Console.WriteLine(context.Get<double>(Graph.Key("Add1", "Out")));
        }
    }
}
