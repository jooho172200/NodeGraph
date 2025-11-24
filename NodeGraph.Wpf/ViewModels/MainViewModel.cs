using NodeGraph.Core.Contexts;
using NodeGraph.Core.Executions;
using NodeGraph.Core.Graphs;
using NodeGraph.Core.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Wpf.ViewModels
{
    class MainViewModel
    {
        public ObservableCollection<NodeViewModel> Nodes { get; }

        public double Result { get; }

        public MainViewModel()
        {
            var g = new Graph();

            g.AddNode("ConstX", new ConstNode("ConstX", 3.0, "Set x"));
            g.AddNode("Const2", new ConstNode("Const2", 2.0, "Set 2"));
            g.AddNode("Mul1", new MulNode("Mul1", "x*2"));
            g.AddNode("Const4", new ConstNode("Const4", 4.0, "Set 4"));
            g.AddNode("Add1", new AddNode("Add1", "t+4"));

            g.Connect("Const2", "Out", "Mul1", "B");
            g.Connect("Const4", "Out", "Add1", "B");
            g.Connect("Mul1", "Out", "Add1", "A");
            g.Connect("ConstX", "Out", "Mul1", "A");

            // 2. 한 번 실행해서 결과도 구해보고
            var ctx = new Context();
            var pipe = new PipelineEvaluator(g);
            pipe.Evaluate(ctx);

            Result = ctx.Get<double>(Graph.Key("Add1", "Out")); // 10 나와야 함

            // 3. 그래프의 노드들을 NodeViewModel로 감싸서 컬렉션에 담기
            Nodes = new ObservableCollection<NodeViewModel>(
                g.Nodes.Values.Select(n => new NodeViewModel(n)));
        }
    }
}
