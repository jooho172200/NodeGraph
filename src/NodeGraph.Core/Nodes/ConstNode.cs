using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;

namespace NodeGraph.Core.Nodes
{
    
    public sealed class ConstNode : IComputeNode
    {
        public string Name { get; }
        public string Id { get; }
        public IReadOnlyList<IPort> Inputs {  get; }
        public IReadOnlyList<IPort> Outputs { get; }

        private readonly double _value;

        public ConstNode(string id, double value, string name = "Const")
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));

            _value = value;

            Inputs = Array.Empty<IPort>();
            Outputs = new IPort[]
            {
                new Port("Out", typeof(double), PortDirection.Out),
            };
        }

        public void Evaluate(Context context, IPortKeyResolver keys)
        {
            if(context is null) throw new ArgumentNullException(nameof(context));
            if(keys is null) throw new ArgumentNullException(nameof(keys));

            var outKey = keys.Resolve(Id, "Out");
            context.Set(outKey, _value);
        }
    }
}
