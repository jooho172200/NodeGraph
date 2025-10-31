using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Nodes
{
    public sealed class AssignNode : INode
    {
        public string Name { get; }
        private readonly string _dstKey, _srcKey;

        public AssignNode(string dstKey, string srcKey, string name = "Assign") {
            _dstKey = dstKey;
            _srcKey = srcKey;
            Name = name;
        }

        public void Evaluate(Context context)
        {
            var v = context.Get<double>(_srcKey);
            context.Set<double>(_dstKey, v);
        }

    }
}
