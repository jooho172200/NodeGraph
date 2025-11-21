using NodeGraph.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Graphs
{
    public sealed class GraphPortKeyResolver : IPortKeyResolver
    {
        public string Resolve(string nodeId, string portName)
        {
            if (string.IsNullOrWhiteSpace(nodeId))
                throw new ArgumentException(nameof(nodeId));

            if (string.IsNullOrWhiteSpace(portName))
                throw new ArgumentException(nameof(portName));

            return Graph.Key(nodeId, portName);
        }
    }
}
