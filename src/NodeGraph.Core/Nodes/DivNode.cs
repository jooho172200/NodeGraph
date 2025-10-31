using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Nodes
{
    public sealed class DivNode : INode
    {
        public string Name { get; }
        private readonly string _aKey, _bKey, _outKey;

        public DivNode(string aKey, string bKey, string outKey, string name = "Div")
        {
            _aKey = aKey;
            _bKey = bKey;
            _outKey = outKey;
            Name = name;
        }

        public void Evaluate(Context context)
        {
            var a = context.Get<double>(_aKey);
            var b = context.Get<double>(_bKey);

            if (Math.Abs(b) < 1e-12) throw new DivideByZeroException();

            context.Set<double>(_outKey, a / b);
        }
    }
}