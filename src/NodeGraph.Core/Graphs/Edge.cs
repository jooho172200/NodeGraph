using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Graphs
{
    public record Edge(string FromNodeId, string FromPort, string ToNodeId, string ToPort);
}