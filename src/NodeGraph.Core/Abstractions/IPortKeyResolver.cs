using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.Core.Abstractions
{
    public interface IPortKeyResolver
    {
        string Resolve(string nodeId, string portName);
    }
}
