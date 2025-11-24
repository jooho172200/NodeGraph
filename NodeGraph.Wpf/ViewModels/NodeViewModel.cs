using NodeGraph.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Wpf.ViewModels
{
    class NodeViewModel
    {
        public string Id { get; }
        public string Name { get; }

        public IReadOnlyList<PortViewModel> Inputs { get; }
        public IReadOnlyList<PortViewModel> Outputs { get; }

        public NodeViewModel(IComputeNode node, string? displayName = null)
        {
            Id = node.Id;
           Name = displayName ?? node.Id;

            Inputs = node.Inputs.Select(p => new PortViewModel(node.Id, p)).ToList();

            Outputs = node.Outputs.Select(p => new PortViewModel(node.Id, p)).ToList();

        }
    }
}
