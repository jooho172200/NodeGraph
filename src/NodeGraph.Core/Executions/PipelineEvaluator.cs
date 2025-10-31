using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;

namespace NodeGraph.Core.Executions
{
    public sealed class PipelineEvaluator
    {
        private readonly List<INode> _nodes = new();

        public void Add(INode node) => _nodes.Add(node);
        public void Clear()=> _nodes.Clear();

        public void Evaluate(Context context)
        {
            foreach (var node in _nodes) node.Evaluate(context);
        }
    }
}
