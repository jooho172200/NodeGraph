using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Nodes
{
    public sealed class SubNode : INode
    {
        public string Name { get; }
        private readonly string _aKey, _bKey, _outKey;

        public SubNode(string aKey, string bKey, string outKey, string name = "Sub")
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
            
            context.Set<double>(_outKey, a - b);
        }
    }
}
