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

            // 2. 노드 등록 (IComputeNode 버전)
            //    각 노드는 Id만 넘기고, 포트 이름은 내부에서
            //    "Out", "A", "B" 등으로 고정되어 있다고 가정.
            g.AddNode("ConstX", new ConstNode("ConstX", 3.0, "Set x"));
            g.AddNode("Const2", new ConstNode("Const2", 2.0, "Set 2"));
            g.AddNode("Mul1", new MulNode("Mul1", "x*2"));
            g.AddNode("Const4", new ConstNode("Const4", 4.0, "Set 4"));
            g.AddNode("Add1", new AddNode("Add1", "t+4"));

            // 3. 엣지 연결 (등록/파일상 순서와 무관)
            //    포트 이름은 각 노드에서 정의한 것과 일치해야 함.
            g.Connect("Const2", "Out", "Mul1", "B");
            g.Connect("Const4", "Out", "Add1", "B");
            g.Connect("Mul1", "Out", "Add1", "A");
            g.Connect("ConstX", "Out", "Mul1", "A");

            // 4. 컨텍스트 생성 + 파이프라인 실행
            Context context = new Context();
            PipelineEvaluator pipe = new PipelineEvaluator(g);

            pipe.Evaluate(context);

            // 5. 결과 출력
            //    내부에서도 (nodeId, portName)을 Graph.Key로 매핑하므로
            //    여기서도 같은 규칙으로 읽어오면 됨.
            double result = context.Get<double>(Graph.Key("Add1", "Out"));
            Console.WriteLine(result); // 기대값: 10
        }
    }
}
