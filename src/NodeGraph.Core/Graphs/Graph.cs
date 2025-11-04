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

        public void AddNode(string nodeId, INode node) => Nodes[nodeId] = node;

        public void Connect(string fromNodeId, String fromPort, string toNodeId, string toPort) => Edges.Add(new Edge(fromNodeId, fromPort, toNodeId, toPort));

        // context key 규칙 한 곳에서 통일
        public static string Key(string nodeId, string portName) => $"{nodeId}.{portName}";
    }
}