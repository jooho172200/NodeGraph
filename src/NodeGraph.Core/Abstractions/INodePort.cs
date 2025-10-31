using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Abstractions
{
    public enum PortDirection { In, Out }

    public interface INodePort
    {
        String Name { get; }
        System.Type ValueType { get; }
        PortDirection Direction { get; }

    }
}