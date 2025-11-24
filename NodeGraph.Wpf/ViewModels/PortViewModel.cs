using NodeGraph.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Wpf.ViewModels
{
    class PortViewModel
    {
        public string NodeId { get; }
        public string Name { get; }
        public PortDirection Direction { get; }
        public string TypeName { get; }

        public PortViewModel(string nodeId, IPort port)
        {
            NodeId = nodeId;
            Name = port.Name;
            Direction = port.Direction;
            TypeName = port.ValueType.Name;
        }

    }
}
