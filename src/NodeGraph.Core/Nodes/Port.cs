using NodeGraph.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Nodes
{
    public sealed class Port : IPort
    {
        public string Name { get; }

        public Type ValueType { get; }

        public PortDirection Direction { get; }

        public Port(string name, Type valueType, PortDirection direction)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
            Direction = direction;
        }
    }
}
