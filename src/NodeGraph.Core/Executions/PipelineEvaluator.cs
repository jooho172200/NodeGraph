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
        private bool _strict;

        public PipelineEvaluator(Graph graph, bool strict=true)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _strict = strict;
        }

        public void Evaluate(Context context)
        {
            if(context is null) throw new ArgumentNullException(nameof(context));

            var(order, hasCycle, cycleSample) = TopoSortWithCycleSample(_graph.Nodes.Keys, _graph.Edges);

            if (hasCycle)
                throw new InvalidOperationException($"Cycle detected among nodes: {string.Join(" ->", cycleSample)}");

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

                        if (!context.TryGet<object>(srcKey, out var v))
                        {
                            if (_strict)
                            {
                                throw new InvalidOperationException(
                                    $"Missing value at '{srcKey}' before evaluating '{nodeId}'. " +
                                    $"Edge requires it for '{e.ToNodeId}.{e.ToPort}'. " +
                                    $"(Check connection or source node output key)");
                            }

                            continue;
                        }
                            
                        context.Set(dstKey, v);
                    }
                }

                _graph.Nodes[nodeId].Evaluate(context);
            }

        }

        private static (List<string> order, bool hasCycle, List<string> cycleSample)
            TopoSortWithCycleSample(IEnumerable<string> nodeIds, IEnumerable<Edge> edges)
        {
            var ids = nodeIds.ToList();
            var order = new List<string>(ids.Count);

            // 방어적: 그래프가 비어 있을 수 있음
            if (ids.Count == 0)
                return (order, false, new List<string>());

            var adj = ids.ToDictionary(id => id, _ => new List<string>());
            var indeg = ids.ToDictionary(id => id, _ => 0);

            foreach (var e in edges)
            {
                // 방어적: 알 수 없는 노드가 Edge에 있다면 스킵/예외(정책에 따라)
                if (!adj.ContainsKey(e.FromNodeId) || !adj.ContainsKey(e.ToNodeId))
                    throw new InvalidOperationException(
                        $"Edge references unknown node(s): {e.FromNodeId} -> {e.ToNodeId}");

                adj[e.FromNodeId].Add(e.ToNodeId);
                indeg[e.ToNodeId]++;
            }

            var q = new Queue<string>(indeg.Where(p => p.Value == 0).Select(p => p.Key));

            while (q.Count > 0)
            {
                var u = q.Dequeue();
                order.Add(u);
                foreach (var v in adj[u])
                    if (--indeg[v] == 0) q.Enqueue(v);
            }

            if (order.Count == ids.Count)
                return (order, false, new List<string>());

            // indeg>0 노드 몇 개를 샘플로 노출
            var cycleSample = indeg.Where(p => p.Value > 0).Select(p => p.Key).Take(8).ToList();
            return (order, true, cycleSample);
        }

        //private static List<string>? TopoSort(IEnumerable<string> nodeIds, IEnumerable<Edge> edges)
        //{
        //    var ids = nodeIds.ToList();
        //    var adj = ids.ToDictionary(id => id, _ => new List<string>());
        //    var indeg = ids.ToDictionary(id => id, _ => 0);

        //    foreach (var e in edges)
        //    {
        //        adj[e.FromNodeId].Add(e.ToNodeId);
        //        indeg[e.ToNodeId]++;
        //    }

        //    var q = new Queue<string>(indeg.Where(p => p.Value == 0).Select(p => p.Key));
        //    var res = new List<string>();

        //    while (q.Count > 0)
        //    {
        //        var u = q.Dequeue();
        //        res.Add(u);
        //        foreach (var v in adj[u])
        //            if (--indeg[v] == 0) q.Enqueue(v);
        //    }

        //    return res.Count == ids.Count ? res : null; // 사이클이면 null
        //}

    }
}
