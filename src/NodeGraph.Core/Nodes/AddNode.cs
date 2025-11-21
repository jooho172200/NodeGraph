using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Nodes
{
    public sealed class AddNode : IComputeNode
    {
        public string Name { get; }
        public string Id {  get; }
        public IReadOnlyList<IPort> Inputs { get; }
        public IReadOnlyList<IPort> Outputs { get; }

        public AddNode(string id, string name = "Add")
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentException(nameof(name));

            Inputs = new IPort[]
            {
                new Port("A", typeof(double), PortDirection.In),
                new Port("B", typeof(double), PortDirection.In)
            };

            Outputs = new IPort[]
            {
                new Port("Out", typeof(double), PortDirection.Out)
            };
        }

        public void Evaluate(Context context, IPortKeyResolver keys)
        {
            if(context is null) throw new ArgumentNullException(nameof(context));
            if(keys is null) throw new ArgumentNullException(nameof(keys));

            var aKey = keys.Resolve(Id, "A");
            var bKey = keys.Resolve(Id, "B");
            var outKey = keys.Resolve(Id, "Out");

            var a = context.Get<double>(aKey);
            var b = context.Get<double>(bKey);

            context.Set<double>(outKey, a + b);
        }
    }
}
