using NodeGraph.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Graphs
{
    public sealed class Graph
    {
        public Dictionary<string, INode> Nodes { get; } = new();
        public List<Edge> Edges { get; } = new();

        public void AddNode(string nodeId, INode node)
        {
            if (string.IsNullOrWhiteSpace(nodeId))
                throw new ArgumentException("nodeId is null or empty");

            if(node is null) throw new ArgumentException(nameof(node));

            if (Nodes.ContainsKey(nodeId))
                throw new InvalidOperationException($"Duplicate node Id: '{nodeId}'");

            Nodes[nodeId] = node;
        }

        public void Connect(string fromNodeId, String fromPort, string toNodeId, string toPort)
        {
            if(!Nodes.ContainsKey(fromNodeId))
                throw new InvalidOperationException($"Unknown source node Id: '{fromNodeId}'");

            if (!Nodes.ContainsKey(toNodeId))
                throw new InvalidOperationException($"Unknown target node Id: '{toNodeId}'");

            bool duplicate = Edges.Any(e =>
                e.FromNodeId == fromNodeId && e.FromPort == fromPort &&
                e.ToNodeId == toNodeId && e.ToPort == toPort);
            if (duplicate) return;

            bool alreadyDriven = Edges.Any(e => e.ToNodeId == toNodeId && e.ToPort == toPort);
            if (alreadyDriven)
                throw new InvalidOperationException($"Input already driven: {toNodeId}.{toPort}");

            Edges.Add(new Edge(fromNodeId, fromPort, toNodeId, toPort));
        }
            
        // context key 규칙 한 곳에서 통일
        public static string Key(string nodeId, string portName) => $"{nodeId}.{portName}";
    }
}