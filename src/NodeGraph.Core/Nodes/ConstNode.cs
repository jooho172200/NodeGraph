using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;

namespace NodeGraph.Core.Nodes
{
    
    public sealed class ConstNode : INode
    {
        public string Name { get; }
        private readonly double _value;
        private readonly string _outKey;

        public ConstNode(double value, string outKey, string name = "Const")
        {
            _value = value;
            _outKey = outKey;
            Name = name;
        }

        public void Evaluate(Context context) => context.Set(_outKey, _value);

    }
}
