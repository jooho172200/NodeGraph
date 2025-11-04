using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Abstractions
{
    public interface IComputeNode
    {
        string Id { get; }
        IReadOnlyList<IPort> Inputs { get; }
        IReadOnlyList<IPort> Outputs { get; }

        void Evaluate(Contexts.Context ctx, IPortKeyResolver keys);
    }
}
