using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeGraph.Core.Abstractions;
using NodeGraph.Core.Contexts;
using NodeGraph.Core.Graphs;

namespace NodeGraph.Core.Executions
{
    public sealed class PipelineEvaluator
    {
        private readonly Graph _graph;

        public PipelineEvaluator(Graph graph)
        {
            _graph = graph;
        }

        public void Evaluate(Context context)
        {
            var order = TopoSort(_graph.Nodes.Keys, _graph.Edges);

            if (order == null) throw new InvalidOperationException("Cycle Detected");

            var incoming = _graph.Edges
                .GroupBy(e  => e.ToNodeId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach ( var nodeId in order)
            {
                if(incoming.TryGetValue(nodeId, out var inEdges))
                {
                    foreach( var e in inEdges)
                    {
                        var srcKey = Graph.Key(e.FromNodeId, e.FromPort);
                        var dstKey = Graph.Key(e.ToNodeId, e.ToPort);

                        if (context.TryGet<object>(srcKey, out var v)) context.Set(dstKey, v);
                    }
                }

                _graph.Nodes[nodeId].Evaluate(context);
            }

        }

        private static List<string>? TopoSort(IEnumerable<string> nodeIds, IEnumerable<Edge> edges)
        {
            var ids = nodeIds.ToList();
            var adj = ids.ToDictionary(id => id, _ => new List<string>());
            var indeg = ids.ToDictionary(id => id, _ => 0);

            foreach (var e in edges)
            {
                adj[e.FromNodeId].Add(e.ToNodeId);
                indeg[e.ToNodeId]++;
            }

            var q = new Queue<string>(indeg.Where(p => p.Value == 0).Select(p => p.Key));
            var res = new List<string>();

            while (q.Count > 0)
            {
                var u = q.Dequeue();
                res.Add(u);
                foreach (var v in adj[u])
                    if (--indeg[v] == 0) q.Enqueue(v);
            }

            return res.Count == ids.Count ? res : null; // 사이클이면 null
        }

    }
}
