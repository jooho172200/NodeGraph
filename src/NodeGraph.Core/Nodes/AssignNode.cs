using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Nodes
{
    public sealed class AssignNode : IComputeNode
    {
        public string Name { get; }
        public string Id { get; }
        public IReadOnlyList<IPort> Inputs { get; }
        public IReadOnlyList<IPort> Outputs { get; }
                
        public AssignNode(string id, string name = "Assign") {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));

            Inputs = new IPort[]
            {
                new Port("Src", typeof(double), PortDirection.In)
            };

            Outputs = new IPort[]
            {
                new Port("Out", typeof (double), PortDirection.Out)
            };

        }

        public void Evaluate(Context context, IPortKeyResolver keys)
        {
            if(context is null) throw new ArgumentNullException(nameof(context));
            if(keys is null) throw new ArgumentNullException(nameof(keys));

            var srcKey = keys.Resolve(Id, "Src");
            var outKey = keys.Resolve(Id, "Out");

            var v = context.Get<double>(srcKey);
            context.Set<double>(outKey, v);
        }
    }
}
