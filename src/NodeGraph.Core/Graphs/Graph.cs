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
            // 오류 방지 정규화
            if (fromPort is null) throw new ArgumentNullException(nameof(fromPort));
            if (toPort is null) throw new ArgumentNullException(nameof(toPort));
            fromPort = fromPort.Trim();
            toPort = toPort.Trim();
            if (fromPort.Length == 0) throw new ArgumentException("fromPort is empty", nameof(fromPort));
            if (toPort.Length == 0) throw new ArgumentException("toPort is empty", nameof(toPort));

            // 노드 존재 검사
            if (!Nodes.ContainsKey(fromNodeId))
                throw new InvalidOperationException($"Unknown source node Id: '{fromNodeId}'");

            if (!Nodes.ContainsKey(toNodeId))
                throw new InvalidOperationException($"Unknown target node Id: '{toNodeId}'");

            // self-loop 금지(같은 노드의 같은 포트)
            if (fromNodeId == toNodeId && string.Equals(fromPort, toPort, StringComparison.Ordinal))
                throw new InvalidOperationException($"Self-loop not allowed: {fromNodeId}.{fromPort}");

            // 중복 엣지 방지
            bool duplicate = Edges.Any(e =>
                e.FromNodeId == fromNodeId && e.FromPort == fromPort &&
                e.ToNodeId == toNodeId && e.ToPort == toPort);
            if (duplicate) return;

            // 동일 입력 포트 단일 드라이버 보장
            bool alreadyDriven = Edges.Any(e => e.ToNodeId == toNodeId && e.ToPort == toPort);
            if (alreadyDriven)
                throw new InvalidOperationException($"Input already driven: {toNodeId}.{toPort}");

            Edges.Add(new Edge(fromNodeId, fromPort, toNodeId, toPort));
        }
            
        // context key 규칙 한 곳에서 통일
        public static string Key(string nodeId, string portName) => $"{nodeId}.{portName}";
    }
}